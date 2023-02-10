
import React from "react"
import DatapageLayout from "../PageLayout"
import { Loading } from "../../Components/appCommon"
import { StdButton } from "../../Components/common"
import "../../styles/volunteer.scss"
export default class Volunteer extends React.Component {
    state={
        content:null,
        headers:[],
        loading:true,
        settings: {},
        error: "",
    }

    settings ={
        title:"Volunteers",
        primaryColor: "#a6192e",
        accentColor: "#94795d",
        textColor: "#ffffff",
        textColorInvert: "#606060",
        api: "/api/Volunteer/",
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

    update = async (data) =>{
        console.log(data);
        return fetch(this.settings.api + "UpdateAndFetch/" + data.UserId , {
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
        await this.update(data).then((content)=>{
            if(content.success){
                this.setState({
                    error:"",
                });
                return true;
            }else{
                this.setState({
                    error:content.message,
                })
                return false;
            }
        })
    }

    approve = async (volunteerId, employee) =>{
        console.log(volunteerId);
        console.log(employee);
        return fetch(this.settings.api + "Approve/" + volunteerId , {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(employee)
        }).then(async res => {
            return res.json();
        });
    }

    handleApprove = async (volunteerId, employee) =>{
        await this.approve(volunteerId, employee).then(async (content)=>{
            if(content.success){
                await this.requestRefresh();
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
    }

    revoke = async (volunteerId) =>{
        console.log(volunteerId);
        return fetch(this.settings.api + "Revoke/" + volunteerId , {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
        }).then(async res => {
            return res.json();
        });
    }

    handleRevoke = async (volunteerId) =>{
        await this.revoke(volunteerId).then(async (content)=>{
            if(content.success){
                await this.requestRefresh();
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
                permissions={this.props.permissions}
                requestError={this.requestError}
                >
                {this.state.content.data.map((item, index) => {
                    if(item.ApprovalStatus === "Approved"){
                        
                        
                        return(
                            <div className="volunteer-extended">
                                <StdButton key={index} className={"secondary"} onClick={()=>{
                                    this.handleRevoke(item.UserId)
                                }}>Revoke</StdButton>
                            </div>
                        )
                    }else if(item.ApprovalStatus === "Pending"){
                        
                        
                        return (
                            <div className="volunteer-extended">
                                <StdButton key={index} onClick={()=>{
                                    this.handleApprove(item.UserId, this.props.user.data)
                                }}>Approve</StdButton>
                            </div>
                        )
                    }else{
                        return null;
                    }
                })}
            </DatapageLayout>
            )
        }
    }
}