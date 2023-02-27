import React from "react";
import { ActionsButton, DivSpacing, IconButton, IconButtonWithText, MultiStepBox, SearchTags, StdButton, SearchButton } from "../Components/common";
import {AccessDeniedPanel, Loading} from "../Components/appCommon";
import { StdInput } from "../Components/input";
import SlideDrawer, { DrawerItemNonLink } from "../Components/sideNav";
import { Cell, ListTable, HeaderRow, ExpandableRow } from "../Components/tableComponents";
import {CSVLink} from "react-csv";
import U from "../Utilities/utilities";

export const searchSuggestions = [
]

const CurrentTags = [
]

const settings = {
}

export default class DatapageLayoutEmpty extends React.Component {
    state = {
        drawerOpen: false,
        expanded: false,
        showBottomMenu: false,
        expansionContent: "",
        expansionComponent: "",
        popUpContent: "",
        data: this.props.data,
        itemsPerPage: 20,
        currentPage: 1,
        pageNumbers: [],
    }
    constructor(props) {
        super(props)

        this.drawerToggleClickHandler = this.drawerToggleClickHandler.bind(this);
        this.setExpansionContent = this.setExpansionContent.bind(this);
        this.handleSeeMore = this.handleSeeMore.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.expand = this.expand.bind(this);
    }
    componentDidMount = async () =>{
        document.title = this.props.settings.title;
        window.addEventListener("resize", this.resize.bind(this));
        this.resize();
        const perms = await this.props.permissions.find(p => p.Module === this.props.settings.title);
        const reformattedPerms = [];
        Object.keys(perms).forEach((perm)=>{
            return perm === "Module" ? null : 
                perms[perm] === true ? reformattedPerms.push(perm) : null
        });
        let extraComponents = [];
        this.props.extraComponents?.length > 0 && 

            this.props.extraComponents.forEach((component)=>{
                
                U.checkSubset(component.requiredPerms,reformattedPerms) && 
                    extraComponents.push(component)
            })
        let tableHeaderActions = await this.populateActions(perms,extraComponents);
        this.setState({
            extraComponents: extraComponents,
            tableHeaderActions: tableHeaderActions,
            data: this.props.data,
            perms : perms,
        })
    }

    populateActions = async (perms,components)=>{
        let tableHeaderActions = [];
        if (perms?.Create && this.props.has?.Create) {
            tableHeaderActions.push({ label: "Add " + this.props.settings.title, onClick: () => { this.setExpansionContent("add") } })
        }
        if (perms?.Delete && this.props.has?.Delete) {
            tableHeaderActions.push({ label: "Delete " + this.props.settings.title, onClick: () => { this.setExpansionContent("del") } })
        }
        if (perms?.Read && this.props.has?.Generate){
            tableHeaderActions.push({ label: "Generate Spreadsheet", onClick: () => { this.setExpansionContent("gen") } })
        }
        components.forEach((component)=>{
            tableHeaderActions.push({label: component.label, onClick: ()=>{this.setExpansionContent(component.key)}})
        })
        return tableHeaderActions;
    }

    rerenderPageNums = (e) => {
        const pageNumbers = [];
        for (let i = 1; i <= Math.ceil(this.state.data.length / e); i++) {
            pageNumbers.push(i);
        }

        this.setState({
            pageNumbers: pageNumbers
        })
    }

    pageNumberClick = (number) => {

        if (number < 1 || number > this.state.pageNumbers.length) {
            return;
        }

        this.setState({
            currentPage: number
        })
    }

    expand() {
        if(this.state.expanded){
            this.setState({
                expanded: !this.state.expanded,
                expansionContent:"",
            })
        }else{
            this.setState({
                expanded: !this.state.expanded,
            })

        }
    }

    setExpansionContent = content => {
        if (this.state.expanded && this.state.expansionContent === content) {
            this.setState({
                expansionContent: "",
                expanded: false,
            })
        } else {

            this.setState({
                expansionContent: content,
                expanded: true,
            })
        }
    }
    drawerToggleClickHandler() {
        this.setState({
            drawerOpen: !this.state.drawerOpen
        })
    }

    resize() {
        if (window.innerWidth <= 760) {
            this.setState({
                showBottomMenu: true
            })
        } else {
            this.setState({
                showBottomMenu: false
            })
        }
    }

    handleSeeMore(content) {
        this.setState({
            popUpContent: content
        })
    }

    handleSearchCallBack =(query) =>{
        this.props.handleSearchCallBack(query);
    }

    handleClose() {
        this.setState({
            popUpContent: ""
        })
    }

    render() {
        return (
            this.state.perms?.Read ? 
            <div className="d-flex flex-column container-fluid listPageContainer h-100">
                {this.props.error !== "" && 
                    <div className="listPageContainer-error">
                        {this.props.error}
                        <IconButton 
                            icon = {<i className="bi bi-x-circle-fill"></i>} 
                            onClick = {()=>this.props.requestError("")}
                        ></IconButton>
                    </div>
                }
                <div className="col-12 d-flex flex-column h-100">
                    
                    <TableHeader actions={
                        this.state.tableHeaderActions
                    } 
                    requestRefresh={this.props.requestRefresh} 
                    fieldSettings={this.props.fieldSettings} 
                    settings={this.props.settings} 
                    showBottomMenu={this.state.showBottomMenu} 
                    handles={this.setExpansionContent} 
                    persist={this.state.showBottomMenu} 
                    expanded={this.state.expanded} 
                    component={this.state.expansionContent} 
                    handleClose={this.expand}
                    handleSearchCallBack = {this.handleSearchCallBack}
                    tagUpdate = {this.handleSearchCallBack}
                    data={this.state.data}
                    perms={this.state.perms}
                    requestError={this.props.requestError}
                    extraComponents={this.state.extraComponents}
                    ></TableHeader>
                    <TableFooter settings={this.props.settings} toggle={this.drawerToggleClickHandler} showBottomMenu={this.state.showBottomMenu}></TableFooter>
                    <DivSpacing spacing={1}></DivSpacing>
                    
                    {this.props.children}
                </div>
                <BottomMenu actions={
                        this.state.tableHeaderActions
                } settings={this.settings} show={this.state.drawerOpen} showBottomMenu={this.state.showBottomMenu} handles={this.setExpansionContent}></BottomMenu>
            </div>
            :
            <AccessDeniedPanel>
            </AccessDeniedPanel>
        )
    }
}
class TableHeader extends React.Component {
    constructor(props) {
        super(props);
        this.toggleSearchBar = this.toggleSearchBar.bind(this);
        this.searchCallBack = this.searchCallBack.bind(this);
        this.onCancelClick = this.onCancelClick.bind(this);
        this.deleteAllTags = this.deleteAllTags.bind(this);
        this.state = {
            classList: "tableRow",
            searchBarExtended: false,
            currentTags: CurrentTags

        }
    }


    onCancelClick = (tagToRemove) =>{
        let newTags = this.state.currentTags.filter((tag)=>{
            return tag !== tagToRemove;
        })
        this.setState({
            currentTags: newTags,
        })
        this.props.tagUpdate(newTags);
    }

    componentDidUpdate(prevProps) {
        if (this.props.persist !== prevProps.persist) {
            this.setState({
                searchBarExtended: true,
            })
        }
    }

    deleteAllTags = () => {
        this.setState({
            currentTags: [],
        })
        this.props.tagUpdate([]);
    }

    toggleSearchBar() {
        this.setState({
            searchBarExtended: !this.state.searchBarExtended,
        })
    }

    searchCallBack(query) {
        this.props.handleSearchCallBack(query);
    }

    render() {


        return (
            <div className="tableHeader">
                <div className={"tableHeaderActions " + (this.props.component === "" ? "borderRadius" : "topBorderRadius")}>
                    <div className="d-flex justify-content-end align-items-center">
                        {this.props.showBottomMenu ? <div /> :
                            <div className="tableTitleContainer">
                                <div className="tableTitlePulseAnimation-1" style={this.state.searchBarExtended ? { "--ScaleMultiplier": .75 } : { "--ScaleMultiplier": 2 }}>
                                </div>
                                <div className="tableTitlePulseAnimation-2" style={this.state.searchBarExtended ? { "--ScaleMultiplier": .75 } : { "--ScaleMultiplier": 2 }}>
                                </div>
                                <div className="tableTitlePulseAnimation-3" style={this.state.searchBarExtended ? { "--ScaleMultiplier": .75 } : { "--ScaleMultiplier": 2 }}>
                                </div>
                                <span className="tableTitle">{this.props.settings.title}</span>
                            </div>}
                        <SearchBar className={"searchHotBar"} onClick={this.toggleSearchBar} toggleTagMacros={this.props.handles} searchCallBack={this.searchCallBack} persist={this.props.showBottomMenu}>

                        </SearchBar>
                        <IconButton title={"Refresh"} size={"48px"} icon={
                            <i className="bi bi-arrow-clockwise" onClick={this.props.requestRefresh}></i>
                        }>
                        </IconButton>
                        {this.props.showBottomMenu ? "" :
                            <div>
                                <TableQuickAction handles={this.props.handles}
                                    actions={this.props.actions}></TableQuickAction></div>}
                    </div>
                </div>
                <HeaderExpansion 
                perms = {this.props.perms}
                settings={this.props.settings} 
                requestRefresh={this.props.requestRefresh} 
                fieldSettings={this.props.fieldSettings} 
                expanded={this.props.expanded} 
                component={this.props.component}
                handleClose={this.props.handleClose} 
                data = {this.props.data}
                requestError = {this.props.requestError}
                extraComponents = {this.props.extraComponents}
                actions={this.props.actions}
                >
                </HeaderExpansion>
                <DivSpacing spacing={1}></DivSpacing>
            </div>
        )
    }
}
TableHeader.defaultProps = {
    component: "",
}

class HeaderExpansion extends React.Component {
    state={
        currentStep: 0,
        steps: [],
        expanded:false,
    }
    componentDidMount(){
        let steps = {}
        let componentsToRender = [];
        if(this.props.perms.Create){
            steps[Object.keys(steps).length] = "add"
            componentsToRender.push(<AddEntry settings={this.props.settings} requestRefresh={this.props.requestRefresh} fieldSettings = {this.props.fieldSettings} requestError={this.props.requestError}></AddEntry>)
        }
        if(this.props.perms.Delete){
            steps[Object.keys(steps).length] = "del"
            componentsToRender.push(<DeleteEntry settings={this.props.settings} requestRefresh={this.props.requestRefresh} fieldSettings = {this.props.fieldSettings} requestError={this.props.requestError}></DeleteEntry>)
        }
        steps[Object.keys(steps).length] = "gs"
        componentsToRender.push(<GenerateSpreadsheet settings={this.props.settings} requestRefresh={this.props.requestRefresh} fieldSettings = {this.props.fieldSettings} data={this.props.data} requestError={this.props.requestError}></GenerateSpreadsheet>)
        this.props.extraComponents.forEach((component)=>{
            steps[Object.keys(steps).length] = component.key
            componentsToRender.push(component.component)
        })
        this.setState({
            steps: steps,
            componentsToRender: componentsToRender,
            currentStep: this.getKeyByValue(steps, this.props.component),
        })
    }

    getKeyByValue(obj, value){
        let key = Object.keys(obj).find(key => obj[key] === value);
        return parseInt(key);
    }

    componentDidUpdate(prevProps) {
        if(prevProps.component != this.props.component){
            this.setState({
                currentStep: this.getKeyByValue(this.state.steps, this.props.component),
            })
        }
        if(prevProps.expanded != this.props.expanded){
            this.setState({
                expanded: this.props.expanded,
            })
        }
    }

    render() {
            return (
                this.state.expanded?
                <HeaderExpansionPane handleClose={this.props.handleClose} title={this.props.actions[this.state.currentStep].label}>
                    <MultiStepBox currentStep = {this.state.currentStep} steps={this.state.steps}>
                        {this.state.componentsToRender.map((component, index) => {
                            return (React.cloneElement(component, {key: index}))
                        })}
                    </MultiStepBox>
                </HeaderExpansionPane>
                :
                <div></div>
            )

    }
}
class HeaderExpansionPane extends React.Component {
    render() {
        return (
            <div className="p-4 headerExpansionPane">
                <div className={"panelCloseBtn d-flex justify-content-between align-items-center"}>
                    <h1>{this.props.title}</h1>
                    <IconButtonWithText className={"invert"} icon={<i className="bi bi-x"></i>} onClick={this.props.handleClose} label={"Close"}></IconButtonWithText>
                </div>
                {this.props.children}
            </div>
        )
    }


}

class AddEntry extends React.Component{
    state = {
        courseToAdd: {},
    }

    onChange = (field, value) => {
        var tempCourse = this.state.courseToAdd;
        tempCourse[field] = value;
        this.setState({
            courseToAdd: tempCourse
        })
    }

    uploadFile = async (file) => {
        console.log(file);
        const formData = new FormData();
        formData.append("file", file.FileUrl);
        
        return await fetch("/api/File/Upload",
            {
                method: "POST",
                body: formData,
            }
        ).then((res) => {
            console.log(res);
            return res.json();
        }).catch(err => {
            console.log(err);
        })
    }

    createCourse = async (courseToAdd) => {

        return fetch(this.props.settings.api + "Create", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(courseToAdd),
        }).then((res => {
            return res.json();
        })).catch((err) => {
            console.log(err);
        })
    }

    handleCourseCreation = async (e) => {
        e.preventDefault();
        var courseToAdd = this.state.courseToAdd;
        var fileUploadFields = [];
        
        for(const field of Object.keys(this.props.fieldSettings)){
            if (this.props.fieldSettings[field].type === "file") {
                fileUploadFields.push(field);
            }
        }

        for(const field of fileUploadFields){
            try {
                const res = await this.uploadFile(courseToAdd[field]);
                if(res.success){
                    courseToAdd[field] = res.data;
                }
            }catch(e){
                this.props.requestError(e);
            }
        }
        try {
            const res = await this.createCourse(courseToAdd);
            if(res.success){
                this.props.requestRefresh();
            }else{
                this.props.requestError(res.message);
            }
        }catch(e){
            this.props.requestError(e);
        }
    }

    render(){
        return (
            <div className="container-fluid addEntry">
                <form className={"addEntry-inputFields"} onSubmit={this.handleCourseCreation}>
                {Object.keys(this.props.fieldSettings).map(
                    (key, index) => {
                        return (this.props.fieldSettings[key].primaryKey? "" : 
                            <StdInput 
                            label = {this.props.fieldSettings[key].displayLabel}
                            type={this.props.fieldSettings[key].type}
                            enabled = {true}
                            fieldLabel={key}
                            onChange = {this.onChange}
                            options={this.props.fieldSettings[key].options}
                            dateFormat = {this.props.fieldSettings[key].dateFormat}
                            allowEmpty = {true}
                            toolTip = {this.props.fieldSettings[key].toolTip}
                            >
                            </StdInput>)
                    }
                )}
                <StdButton type={"submit"}>Submit</StdButton>
            
                </form>
                </div>
        )
    }
}

class DeleteEntry extends React.Component{
    state = {
        courseToDelete: {},
    }

    onChange = (field, value) => {
        var tempCourse = this.state.courseToDelete;
        tempCourse[field] = value;
        this.setState({
            courseToDelete: tempCourse
        })
    }

    deleteCourse = async (courseToDelete) => {
        console.log(courseToDelete);
        return fetch(this.props.settings.api + "Delete", {
            method: "Delete",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(courseToDelete),
        }).then((res => {
            return res.json();
        }));
    }

    handleCourseDeletion = async (e) => {
        e.preventDefault();
        await this.deleteCourse(this.state.courseToDelete).then((content) => {
            if(content.success){
                this.props.requestRefresh();
            }else{
                this.props.requestError(content.message);
            }
        })
    }

    render(){
        return (
            <div className="container-fluid deleteEntry">
                <form className={"deleteEntry-inputFields"} onSubmit={this.handleCourseDeletion}>
                {Object.keys(this.props.fieldSettings).map(
                    (key, index) => {
                        return (this.props.fieldSettings[key].primaryKey? 
                            <StdInput 
                            label = {this.props.fieldSettings[key].displayLabel}
                            type={"text"}
                            enabled = {true}
                            fieldLabel={key}
                            onChange = {this.onChange}
                            options={this.props.fieldSettings[key].options}
                            dateFormat = {this.props.fieldSettings[key].dateFormat}
                            >
                            </StdInput> : "")
                    }
                )}
                <StdButton type={"submit"}>Submit</StdButton>
            
                </form>
            </div>
        )
    }
}

class GenerateSpreadsheet extends React.Component{
    state={
        columns: [],
        spreadsheetReady: false,
    }
    
    componentDidMount(){
        let columns = [];
        for(var i = 0; i < Object.keys(this.props.fieldSettings).length; i++){
            columns.push(
                {
                    label: Object.keys(this.props.fieldSettings)[i],
                    key: Object.keys(this.props.fieldSettings)[i],
                }
            );
        }
        this.setState({
            columns: columns
        });
    }

    reOrderColumns = (index, direction) => {
        var tempColumns = this.state.columns;
        if(direction === "up"){
            if(index > 0){
                var temp = tempColumns[index];
                tempColumns[index] = tempColumns[index - 1];
                tempColumns[index - 1] = temp;
            }
        } else {
            if(index < tempColumns.length - 1){
                var temp = tempColumns[index];
                tempColumns[index] = tempColumns[index + 1];
                tempColumns[index + 1] = temp;
            }
        }
        this.setState({
            columns: tempColumns
        });
    }

    generateSpreadsheet = () =>{
        this.setState({
            spreadsheetReady : false
        })

        // Fake loading time to show false sense of progress
        setTimeout(() => {
            this.setState({
                spreadsheetReady : true
            })}, 1000);
    }

    render(){
        return (
            <div className="container-fluid generate-spreadsheet">
                <div className="column-order">
                    {this.state.columns.map((column, index) => {
                        return <div className="column">
                            <div className="column-order-buttons">
                                <IconButton className={"invert"} icon={<i className="bi bi-arrow-up"></i>} onClick={() => this.reOrderColumns(index, "up")}></IconButton>
                                <IconButton className={"invert"} icon={<i className="bi bi-arrow-down"></i>} onClick={() => this.reOrderColumns(index, "down")}></IconButton>
                            </div>
                            <div className="column-name">{column.label}</div>
                        </div>
                    })}     
                </div>
                <div className="generate-actions">
                    <StdButton onClick={() => this.generateSpreadsheet()}>
                        Generate Spreadsheet
                    </StdButton>

                    {this.state.spreadsheetReady ?
                    
                    <CSVLink data={this.props.data} className={"forget-password"} headers={this.state.columns} filename={this.props.settings.title + ".csv"}>Download</CSVLink>
                    :
                    <div className="spinner-border" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </div>
                    }    
                </div>
            </div>
        )
    }

    
}

class TableFooter extends React.Component {
    render() {
        return (
            this.props.showBottomMenu ?
                <ActionsButton className={"fixed-bottom footer-Btn"} onClick={this.props.toggle}></ActionsButton> : ""
        )
    }
}

class TableQuickAction extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showActionsMenu: false,
            actionsMenuClasses: "actionsMenu",
            actionsButtonClasses: "actionsMenuToggle"
        }
    }
    render() {

        return (
            <div className="d-flex quickActionsPanel align-items-center">
                <ActionsButton onClick={() => {
                    if (this.state.showActionsMenu) {
                        this.setState({
                            showActionsMenu: !this.state.showActionsMenu,
                            actionsMenuClasses: "actionsMenu cardBg",
                            actionsButtonClasses: "actionsMenuToggle"
                        })
                    } else {

                        this.setState({
                            showActionsMenu: !this.state.showActionsMenu,
                            actionsMenuClasses: "actionsMenu cardBg showActions",
                            actionsButtonClasses: "actionsMenuToggle active"
                        })
                    }
                }}>
                    {this.props.actions.map((action, index) => {
                        return (
                            <DrawerItemNonLink key={index} label={action.label} onClick={action.onClick}></DrawerItemNonLink>
                        )

                    })}
                </ActionsButton>
            </div>
        )
    }
}

class BottomMenu extends React.Component {
    render() {
        return (
            this.props.showBottomMenu ?
                <SlideDrawer show={this.props.show} direction={"bot"} columns={1} settings={settings}>
                    {this.props.actions.map((action, index) => {
                        return (
                            <DrawerItemNonLink key={index} label={action.label} width={"25"} onClick={action.onClick}></DrawerItemNonLink>
                        )

                    })}</SlideDrawer> : <div></div>
        )
    }
}


class SearchBar extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            expanded: false,
            inputClasses: "SearchFieldGroup",
            showToolTip: false,
            searchQuery: "",
            selectedTag: "",
            tagType: "",
            suggestions: this.props.suggestions,
            macrosSuggestion: this.props.suggestions.filter(suggestion => suggestion.type === "macro"),
            specificSuggestion: this.props.suggestions.filter(suggestion => suggestion.type === "specific"),
            multipleSuggestion: this.props.suggestions.filter(suggestion => suggestion.type === "multiple"),
            placeholder: "",
        }
        this.searchInput = React.createRef();
        this.toggle = this.toggle.bind(this);
        this.focus = this.focus.bind(this);
        this.toggleToolTip = this.toggleToolTip.bind(this);
        this.searchCallBack = this.searchCallBack.bind(this);
        this.onCancelClick = this.onCancelClick.bind(this);
        this.handleKeydown = this.handleKeydown.bind(this);
        this.state.expanded = this.props.persist ? true : false;
        this.state.inputClasses = this.props.persist ? "SearchFieldGroup if-active" : "SearchFieldGroup";
    }

    componentDidUpdate(prevProps) {
        if (prevProps.persist !== this.props.persist) {
            this.updateAndNotify();
        }
    }

    focus() {
        if (!this.props.persist) {
            this.searchInput.current.focus()
        }
    }

    updateAndNotify() {
        this.setState({
            expanded: true,
            inputClasses: "SearchFieldGroup if-active"
        })
    }

    toggleToolTip = (e) => {
        e.stopPropagation()
        this.setState({
            showToolTip: !this.state.showToolTip
        })
    }
    toggle() {
        this.props.onClick();

        if (this.props.persist) {
            this.searchInput.current.focus();
            return;
        } else {

            if (this.state.expanded) {
                this.setState({
                    expanded: false,
                    inputClasses: "SearchFieldGroup"
                })
            } else {
                this.setState({
                    expanded: true,
                    inputClasses: "SearchFieldGroup if-active"
                })
                this.searchInput.current.focus();
            }
        }

    }

    setSearch(option) {
        this.searchInput.current.value = option;
    }

    setPrimaryInput(tag) {
        this.setState({
            selectedTag: tag.value,
            tagType: tag.type,
        })
        this.searchInput.current.value = ""

        if (tag.value.slice(1) === "gender") {
            this.setState({
                suggestions: [{ value: "Male", label: "Male", type: "" }, { value: "Female", label: "Female", type: "" }],
                placeholder: "Enter a gender",
            })
        }
        this.searchInput.current.focus();
    }

    handleSearchQueryChange = (e) => {
        this.setState({
            searchQuery: e.target.value
        })
    }

    handleKeydown = (e,tag) => {
        if (e.key === "Enter") {
            this.searchCallBack(tag);
        }
    }

    searchCallBack(tag) {
        this.setState({
            selectedTag: "",
            tagType: "",
            suggestions: searchSuggestions,
            placeholder: "",
        })
        this.searchInput.current.value = ""
        this.searchInput.current.focus();
        this.props.searchCallBack(tag);
    }

    onCancelClick() {
        this.setState({
            selectedTag: "",
            tagType: "",
            suggestions: searchSuggestions,
            placeholder: "",
        })
        this.searchInput.current.value = "";
    }

    render() {

        return (
            <div className={this.props.className}>
                <div className={" justify-content-end d-flex align-items-center"}>
                    <div className="searchBar">
                        <SearchButton onClick={this.toggle} className={this.props.invert ? "invert" : ""} icon={<i className="bi bi-search"></i>} toolTip={this.props.toolTip} showToolTip={this.state.showToolTip} onMouseEnter={this.toggleToolTip} onMouseLeave={
                            this.toggleToolTip}></SearchButton>

                    </div>{this.state.selectedTag !== "" &&
                        <SearchTags showEdit={false} onCancelClick={this.onCancelClick} type={this.state.tagType}>{this.state.selectedTag}</SearchTags>
                    }
                    <div className={"d-flex align-items-center " + this.state.inputClasses} onAnimationEnd={this.focus}>
                        <input type={"text"} className={"SearchField"} placeholder={this.state.placeholder} ref={this.searchInput} onChange={this.handleSearchQueryChange} onKeyDown={(e) => this.handleKeydown(e, this.state.searchQuery)}></input>
                    </div>
                </div>
            </div>
        )
    }
}
SearchBar.defaultProps = {
    invert: false,
    suggestions: searchSuggestions,
}
