import moment from "moment";
import React from "react"
import QRCode from "react-qr-code";
import { AppPageContainer, Loading } from "../../Components/appCommon";
import { Shimmer } from "../../Components/common";
import { StdInput } from "../../Components/input";
import "../../styles/volunteer-home.scss"

export default class VolunteerHome extends React.Component{
    state={
        loading:true,
        volunteerWork:[],
    }

    componentDidMount = async () =>{
        await this.getVolunteerWork().then((data)=>{
            if(data.success){
                this.setState({
                    volunteerWork:data.data,
                })
            }
        })
        this.setState({
            loading:false,
        })
    }

    getVolunteerWork = async () => {
        var loggedInVol = this.props.user.data;
        console.log(loggedInVol.UserId);
        return fetch("/api/VolunteerWork/GetByVolunteerId/" + loggedInVol.UserId ,{
            method: "GET",
            headers:{
                "Content-Type": "application/json",
            },
        }).then(response => {
            return response.json();
        });
    }


    render(){
        return(
            this.state.loading?
            <Loading></Loading>
            :
            
                <div className="container-fluid">
                    {this.props.user.data.ApprovalStatus == "Approved"?
                    
                    <VolunteerWorkList data={this.state.volunteerWork}></VolunteerWorkList>
                    :
                    <VolunteerPending></VolunteerPending>}
                </div>
        )
    }
}

class VolunteerPending extends React.Component{
    render(){
        return(
            <div className="container-fluid volunteer-pending">
                <div className="card-bg">
                    <div className="header">
                    Approval still pending
                    </div>
                    <div className="content">
                    
                    <div className="pending-message">
                        
                        <i className="bi bi-emoji-smile-fill pending-message-icon"></i>
                        <div className="pending-message-text">
                            Thank you for your interest in volunteering with us. We are currently reviewing your application and will get back to you shortly.
                        </div>
                    </div>
                    </div>

                </div>
            </div>
        )
    }
}

class VolunteerWorkList extends React.Component{
    state={
        loading:true,
        excludes:["SupervisingEmployee","VolunteerId"],
        data:this.props.data,
    }

    componentDidMount = async () =>{
        this.getVolunteerWorkSettings().then((data)=>{
            if(data.success){
                this.setState({
                    settings:data.data,
                    fieldSettings:data.data.FieldSettings,
                    columnSettings:data.data.ColumnSettings,
                    loading:false,
                })
            }else{
                this.setState({
                    loading:false,
                })
            }
        });
    }

    getVolunteerWorkSettings = async () => {
        return fetch("/api/VolunteerWork/Settings",{
            method: "GET",
            headers:{
                "Content-Type": "application/json",
            },
        }).then(response => {
            return response.json();
        });
    }

    render(){
        return(
            this.state.loading?
            
            <div className="container-fluid volunteer-work-list-container">
                <div className="list-item header" style={{"--Columns": 4}}>
                    {
                        [1,2,3,4].map((item)=>{
                            return(
                                <div className="w-100">
                                    <Shimmer type={"title"} noPadding={true}></Shimmer>
                                </div>
                            )
                        })
                    }
                </div>
                {
                    [1,2,3].map((item)=>{
                        return(
                            <div className="list-item">
                                <div className="col-md-12 col-12">
                                    <Shimmer type={"content"} noPadding={true}></Shimmer>
                                </div>
                            </div>
                        )
                    })
                }
            </div>
            :
            <div className="container-fluid volunteer-work-list-container">
                <div className="list-item header" style={{"--Columns": (Object.keys(this.state.columnSettings).length - this.state.excludes.length)}}>
                    {Object.keys(this.state.columnSettings).map((key)=>{
                        if(this.state.excludes.includes(key)){
                            return null;
                        }else{
                            
                            return(
                                <div className="header-cell">
                                    {this.state.columnSettings[key].displayHeader}
                                </div>
                            )
                        }
                    })}
                </div>
                {this.props.data.map((item)=>{
                    return(
                        <VolunteerWorkExpandibleRow data={item} excludes={this.state.excludes} columnSettings = {this.state.columnSettings} fieldSettings= {this.state.fieldSettings}></VolunteerWorkExpandibleRow>
                    )
                })}
            </div>
        )
    };
}

class VolunteerWorkExpandibleRow extends React.Component{
    state={
        expanded:false,
    }
    render(){
        return(
            
            <div className="container-fluid p-0">
                <div 
                className={"list-item " + (this.state.expanded ? "active" : "")} 
                style={{"--Columns": Object.keys(this.props.columnSettings).length - this.props.excludes.length}}
                onClick={()=>{
                    this.setState({
                        expanded:!this.state.expanded,
                    })
                }}
                >
                    {Object.keys(this.props.fieldSettings).map((key)=>{
                        if(this.props.excludes.includes(key)){
                            return null;
                        }else{
                            
                            return(
                                <div className="header-cell">
                                    {this.props.fieldSettings[key].type == "dropdown" &&  this.props.data[key] != null &&
                                        this.props.fieldSettings[key].options.find((item)=>{return item.value == this.props.data[key]}).label
                                    }
                                    {this.props.fieldSettings[key].type == "datetime" && this.props.data[key] != null && 
                                        moment(this.props.data[key]).format("DD/MM/YYYY HH:mm a")
                                    }
                                    {this.props.fieldSettings[key].type != "dropdown" && this.props.fieldSettings[key].type != "datetime" &&
                                        this.props.data[key]
                                    }
                                </div>
                            )
                        }
                    })}
                </div>
                <div className={"content " + (this.state.expanded ? "show": "")}>
                    <VolunteerWorkCard fieldSettings = {this.props.fieldSettings} data = {this.props.data}></VolunteerWorkCard>
                </div>
            </div>
        )
    }
}

class VolunteerWorkCard extends React.Component{

    render(){
        return(
            <div className="container-fluid">
                <div className="volunteer-work-content row">
                
                <div className="card-bg col-md-4 col-12">
                    <div className="body">
                        <QRCode value={this.props.data.VolunteerWorkId} style={{maxWidth:"100%", height:"auto", width:"100%"}}></QRCode>
                        <div className="work-card-info">
                            <span className="work-card-title">Work Id</span>
                            <span className="work-card-line">{this.props.data.VolunteerWorkId}</span>
                        </div>
                    </div>
                </div>
                <div className="work-info col-md-8 col-12">
                    <div className="work-info-header">
                        Work Info
                    </div>
                    <div className="work-info-body">
                        <div className="row g-3">
                            
                        {Object.keys(this.props.fieldSettings).map((key)=>{
                            return <div className="col-md-6 col-12">
                            <StdInput
                                editable={false}
                                value={this.props.data[key]}
                                type={this.props.fieldSettings[key].type}
                                options={this.props.fieldSettings[key].options}
                                label={this.props.fieldSettings[key].displayLabel}
                            >
                            </StdInput>
                            </div>
                        })} 
                            
                        </div>
                    </div>
                </div>
            </div>
            </div>
        )
    }
}