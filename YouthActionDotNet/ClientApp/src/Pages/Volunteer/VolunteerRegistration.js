import React from 'react';
import { Loading } from '../../Components/appCommon';
import { StdButton } from '../../Components/common';
import { StdInput } from '../../Components/input';
import "../../styles/volunteer-registration.scss";

async function getPermissions(token){
    var role = token.data.Role;
    if(role === "Employee"){
      role = token.data.EmployeeRole;
    }
    return fetch( "/api/Permissions/GetPermissions/" + role , {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
        },
    }).then(res => {
        console.log(res);
        return res.json();
    });
  }
  
export default class VolunteerRegistration extends React.Component{
    state ={
        loading:true,
        settings: {},        
        excludes:["UserId","ApprovedBy","ApprovalStatus","Role"],
        dataToPush:{},
    }
    
    async componentDidMount(){
        await this.getSettings().then((settings)=>{
            this.setState({
                settings: settings.data,
            })
        })
        
        this.setState({
            loading:false,
        })
    }

    getSettings = async () => {
        
        return await fetch("/api/Volunteer/Settings",{
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            }
        }).then(response => {
            return response.json();
        });
    }

    register = async () =>{
        return await fetch("/api/Volunteer/Register",{
            method: "POST",
            headers:{
                "Content-Type": "application/json",
            },
            body:JSON.stringify(this.state.dataToPush),
        }).then(response => {
            return response.json();
        });
    }

    handleRegister = async () =>{
        const token = await this.register();
        const perms = await getPermissions(token)
        if(token.success){
            console.log(token);
            this.props.setToken(token);
            this.props.setPerms(perms.data.Permission);
        }else{
            console.log(token.message);
        }
    }

    

    onChange = (field, value) =>{
        var dataToPush = this.state.dataToPush;
        dataToPush[field] = value;
        this.setState({
            dataToPush : dataToPush,
        })        
    }


    render(){
        return(
            this.state.loading ? 
            <Loading></Loading>
            :
            <form className='container volunteer-form-container' onSubmit={this.handleRegister}>
                {Object.keys(this.state.settings.FieldSettings).map((key,index)=>{
                const field = this.state.settings.FieldSettings[key];
                
                    return (
                        this.state.excludes.includes(key) ? null :
                        <div className='row'>
                            <div className='col-12'>
                                <StdInput 
                                    label = {field.displayLabel}
                                    type={field.type}
                                    enabled = {true}
                                    
                                    fieldLabel={key}
                                    onChange = {this.onChange}
                                    options={field.options}
                                    dateFormat = {field.dateFormat}
                                    allowEmpty = {true}
                                    toolTip = {field.toolTip}
                                    >
                                </StdInput>
                            </div>
                        </div>
                    )
                    })}
                <StdButton type={"submit"}>Submit</StdButton>
            </form>
        )
    }
}

