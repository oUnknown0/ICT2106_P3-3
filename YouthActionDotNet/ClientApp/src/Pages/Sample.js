
import React from "react"
import DatapageLayoutEmpty from "./PageLayoutEmpty"
import { Loading } from "../Components/appCommon"
import { WeekView } from "../Components/common"

export default class Sample extends React.Component {
    state={
        content:null,
        headers:[],
        loading:true,
        settings: {},
        error: "",
    }

    settings ={
        title:"Sample",
        primaryColor: "#a6192e",
        accentColor: "#94795d",
        textColor: "#ffffff",
        textColorInvert: "#606060",
        api: "/api/User/",
    }

    async componentDidMount(){
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

        // Fetch your settings here to use the default ADD, DELETE, Generate Spreadsheet Behavior
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

    requestRefresh = async () =>{
        this.setState({
            loading:true,
        })

        // do your own refresh here
        await this.getContent().then((content)=>{
            console.log(content);
            this.setState({
                content:content,
                loading:false,
            });
        })
    }

    searchCallBack = (search) => {
        //Implement your search here
        console.log(search);
    }

    render(){
        if(this.state.loading){
            return <Loading></Loading>
        }else{
            
        return(
            <DatapageLayoutEmpty 
                settings={this.settings}
                //fieldSettings={this.state.settings.data.FieldSettings} 
                requestRefresh = {this.requestRefresh}
                error={this.state.error}
                permissions={this.props.permissions}
                handleSearchCallBack={this.searchCallBack}

                // This is the default behavior for the ADD, DELETE, Generate Spreadsheet buttons and REQUIRES the settings to be fetched
                //has={{"Create": true, "Delete": true, "Generate": true}}
                
                // If you do not wish to use the default behavior, you can implement your own using the extra components
                // extraComponents = {...}
                >
                {/* Implement your own content here */}
            </DatapageLayoutEmpty>
            )
        }
    }
}