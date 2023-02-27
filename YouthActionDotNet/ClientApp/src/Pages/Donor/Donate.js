
import React from "react"
import { Loading } from "../../Components/appCommon"
import DatapageLayout from "../PageLayout"
import '../../styles/donorDashboard.css'



export default class Donate extends React.Component {
    state={
        content:null,
        headers:[],
        loading:true,
        settings: {},
        error: "",
    }

    settings ={
        title:"Donate",
        primaryColor: "#a6192e",
        accentColor: "#94795d",
        textColor: "#ffffff",
        textColorInvert: "#606060",
        api: "/api/Donor/",
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
          
            <div className="col-md-12">
                <div className="row">
                <div className="tableHeader p-4">
                <div className="tableHeaderActions ">
                <div className="d-flex justify-content-start align-items-center">
                       
                    <div className="tableTitleContainer">
                        <div className="tableTitlePulseAnimation-1" style={this.state.searchBarExtended ? { "--ScaleMultiplier": .75 } : { "--ScaleMultiplier": 2 }}>
                        </div>
                        <div className="tableTitlePulseAnimation-2"  style={this.state.searchBarExtended ? { "--ScaleMultiplier": .75 } : { "--ScaleMultiplier": 2 }}>
                        </div>
                        <div className="tableTitlePulseAnimation-3" style={this.state.searchBarExtended ? { "--ScaleMultiplier": .75 } : { "--ScaleMultiplier": 2 }}>
                        </div>
                            <span className="tableTitle">Donate Now</span>
                        </div>
                </div>
                </div>
                </div>
                </div>

                <div className="row justify-content-center">
                <div className="col-md-6 ">
                <form>
                    <div class="form-group row mb-3">
                        <label for="selectedProject" class="col-sm-3 col-form-label">Selected Project</label>
                        <div class="col-sm-9">
                        <input type="text" readonly class="form-control-plaintext" id="selectedProject" value="Project X"/>
                        </div>
                    </div>
                    <div class="form-group row mb-3">
                        <label for="donationAmount" class="col-sm-3 col-form-label">Donation Amount</label>
                        <div class="col-sm-9">
                        <input type="text" class="form-control" id="donationAmount" placeholder="Enter Amount"/>
                        </div>
                    </div>
                    <div class="form-group row mb-3">
                        <label for="donationConstraint" class="col-sm-3 col-form-label">Donation Constraint</label>
                        <div class="col-sm-9">
                        <input type="text" class="form-control" id="donationConstraint" placeholder="Enter Constraint"/>
                        </div>
                    </div>
                    <div class="d-flex justify-content-center mt-5">
                    <button type="submit" class="btn btn-primary">Donate Now!</button>
                    </div>
                    </form>
                </div>
                </div>

               

            </div>
            
            )
        }
    }
}