
import React from "react"
import { Loading } from "../../Components/appCommon"
import DatapageLayout from "../PageLayout"

export default class Expense extends React.Component {
    state={
        content:null,
        headers:[],
        loading:true,
        settings: {},
        error: "",
    }

    settings ={
        title:"Expenses",
        primaryColor: "#a6192e",
        accentColor: "#94795d",
        textColor: "#ffffff",
        textColorInvert: "#606060",
        api: "/api/Expense/",
    }

    async componentDidMount(){
        await this.getContent().then((content)=>{
            console.log(content);
            this.setState({
                content:content,
            });
        })

        await this.getSettings().then((settings)=>{
            console.log(settings);
            this.setState({
                settings:settings,
            });
        })

        this.setState({
            loading:false,
        })
    }

    getSettings = async () => {
        // fetches http://...:5001/api/User/Settings
        return fetch(this.settings.api + "Settings" , {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        }).then(res => {
            console.log(res);
            return res.json();
        })
    }

    getContent = async () =>{
        return fetch( this.settings.api + "All" , {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        }).then(res => {
            console.log(res);
            //Res = {success: true, message: "Success", data: Array(3)}
            return res.json();
        });
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
    
    update = async (data) =>{
        console.log(data);
        return fetch(this.settings.api + "UpdateAndFetch/" + data.ExpenseId , {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data)
        }).then(async res => {
            return res.json();
        });
    }

    handleUpdate = async (data) =>{
        var updateData = data;
        var fileUploadFields = [];
        const fieldSettings = this.state.settings.data.FieldSettings;
        for(const field of Object.keys(fieldSettings)){
            if (fieldSettings[field].type === "file") {
                fileUploadFields.push(field);
            }
        }

        for(const field of fileUploadFields){
            try {
                const res = await this.uploadFile(updateData[field]);
                if(res.success){
                    updateData[field] = res.data;
                }
            }catch(e){
                this.props.requestError(e);
            }
        }
        await this.update(data).then((content)=>{
            if(content.success){
                this.setState({
                    error:"",
                })
                return true;
            }else{
                this.setState({
                    error:content.message,
                })
                return false;
            }
        })
        try{
            const res = await this.update(data);
            if(res.success){
                this.setState({
                    error:"",
                })
                return true;
            }else{
                this.setState({
                    error:res.message,
                })
                return false;
            }
        }catch(e){
            this.requestError(e);
        }
    }

    requestRefresh = async () =>{
        this.setState({
            loading:true,
        })
        await this.getContent().then((content)=>{
            console.log(content);
            this.setState({
                content:content,
                loading:false,
            });
        })
    }

    requestError = async (error) =>{
        this.setState({
            error:error,
        })
    }

    render(){
        if(this.state.loading){
            return <Loading></Loading>
        }else{
            
        return(
            <DatapageLayout 
                settings={this.settings}
                fieldSettings={this.state.settings.data.FieldSettings} 
                headers={this.state.settings.data.ColumnSettings} 
                data={this.state.content.data}
                updateHandle = {this.handleUpdate}
                requestRefresh = {this.requestRefresh}
                error={this.state.error}
                requestError = {this.requestError}
                permissions={this.props.permissions}
                >
            </DatapageLayout>
            )
        }
    }
}