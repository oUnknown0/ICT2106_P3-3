import React from "react"
//import { QrReader } from 'react-qr-reader';
import { Link } from "react-router-dom"
import { MultiStepBox, StdButton } from "./common";
import { StdInput } from "./input";
import { QrReader } from "./cameraComponents.tsx";
import "../styles/history.scss";
import moment from "moment";

export class AppButton extends React.Component {
  render() {
    return (
        this.props.link ? 
        
      <Link to ={this.props.link} className="app-button" style={{color: this.props.color}} onClick={this.props.onClick}>
        {this.props.icon}
        <span className="app-button-label">{this.props.label}</span>
      </Link>
      :
      <button className="app-button" style={{color: this.props.color}} onClick={this.props.onClick}>
        {this.props.icon}
        <span className="app-button-label">{this.props.label}</span>
      </button>
    )
  }
}

export class AppPageContainer extends React.Component{

  state = {
    height: window.innerHeight,
  }

  componentDidMount(){
    this.resize();
    window.addEventListener('resize', () => {
      this.resize();
    });

  }

  resize = () =>{
    this.setState({
      height: window.innerHeight,
    })
  }

  render(){
    return(
      <div className={"app-page " + (this.props.nopad? "no-pad" : "")} style={{maxHeight: (this.state.height - 56), gap:(this.props.gap)}}>
        {this.props.children}

      </div>
    )
  }
}

export class QRPage extends React.Component{

  state={
    currmode: 0
  }

  setData=(e)=>{
    this.props.setData(e);
  }
  render(){
    return(
      <div className='CheckInStation-container'>
                        
        {this.props.header && 
        <div className="Station-header">
          <div className="back-button" onClick={this.props.backAction}><i className="bi bi-arrow-left"></i></div>
          {this.props.header}
        </div>
        }
        {this.props.subHeader && 
          <div className="Station-sub-header">
          {/* <div className="back-button" onClick={()=>this.props.navigate("/CheckInStation")}><i className="bi bi-arrow-left"></i></div> */}
            {this.props.subHeader}
          </div>
        }
        <QRScanner step={this.state.currmode} setData={this.setData} manualSettings={this.props.manualSettings} handleSubmit={this.props.handleSubmit}></QRScanner>
        
        <div className="qr-tabs">
          <div className={"qr-tab " + (this.state.currmode === 0 ? "active" : "")} onClick={()=>this.setState({currmode: 0})}>
            <i className="bi bi-qr-code"></i> Scan QR
          </div>
          <div className={"qr-tab " + (this.state.currmode === 1 ? "active" : "")} onClick={()=>this.setState({currmode: 1})}>
          <i className="bi bi-input-cursor-text"></i> Manual Entry
          </div>

        </div>
      </div>
    )
  }
}

export class QRScanner extends React.Component {
  state={
    data:"Not Found",
    steps:{0: "QR Scanner", 1: "Manual Entry"}
  }

  setData = (data) =>{
    this.setState({
      data: data
    })

    this.props.setData?.(data);
  }

  componentWillUnmount(){
    
  }

  
  
  render(){
    return (
      <div className={"qr-container"}>
        
        <MultiStepBox steps={this.state.steps} currentStep={this.props.step}>
          <QRScannerInternal setData={this.setData}></QRScannerInternal>
          <ManualEntryInternal setData={this.setData} settings={this.props.manualSettings} handleSubmit={this.props.handleSubmit}></ManualEntryInternal>
        </MultiStepBox>
      </div>
    );
  }
}


class QRScannerInternal extends React.Component{
  
  state={
    mounted: false,
  }

  setData = (data) =>{
    this.setState({
      data: data
    })

    this.props.setData?.(data);
  }

  componentDidMount(){
    this.setState({
      mounted: true
    })
  }

  componentWillUnmount(){
    this.setState({
      mounted: false
    });
  }

  render(){
    return(
        this.state.mounted ?
        <div className="qr-internal" id={"QR-internal"}>
          <QrReader setData={this.setData}></QrReader>
        </div>
      :
      <Loading></Loading>
    )
  }
}

class ManualEntryInternal extends React.Component{

  setData = (field,data) =>{
    this.setState({
      data: data
    })

    this.props.setData?.(data);
  }

  handleSubmit = () =>{
    this.props.handleSubmit?.();
  }

  render(){
    return(
      
      <div className="manual-internal">
      <StdInput type={this.props.settings.type} enabled={true} label={"Manual Entry"} onChange={this.setData} options={this.props.settings.options}></StdInput>
      <StdButton onClick={()=>this.handleSubmit()}>Submit</StdButton>
    </div>
    )
  }
}

export class HistoryList extends React.Component{
  render(){
    return(
      <div className="history-list">
        <div className="history-list-header">
          {this.props.header? this.props.header : "Update Logs"}
        </div>
        <div className="history-list-items">
          
          {this.props.history.map((item,index)=>{
            if(item.category === 1){
              return <SampleHistoryItem item={item} key={index}></SampleHistoryItem>
            }

            if(item.category === 2){
              return <WasherHistoryItem item={item} key={index}></WasherHistoryItem>
            }

            if(item.category === 3){
              return <DryerHistoryItem item={item} key={index}></DryerHistoryItem>
            }

            if(item.category === 4){
              return <ScopeHistoryItem item={item} key={index}></ScopeHistoryItem>
            }
            
          })}
        </div>
      </div>
    )
  }
}

export class SampleHistoryItem extends React.Component{

   stations = {
    "manual" : "Manual Wash Station",
    "machine" : "Machine Wash Station",
    "dryer" : "Dryer Station",
  }

  actions = {
    "updatesample" : "Updated Sample",
    "createsample" : "Created Sample",
  }

  state={
    actionStation: this.props.item.action.split('-'),
  }
  render(){
    return(
      <div className="history sample">
        <span className="info">
          <Link to={`/History/User/${this.props.item.uid}`} params={{uid:this.props.item.uid}}>{this.props.item.username}</Link>
          <span>{this.actions[this.state.actionStation[0]]}</span>
          <Link to={`/SampleDetails/${this.props.item.relevantID}`}>
          {this.props.item.relevantID}</Link>
          <span>at {this.stations[this.state.actionStation[1]]}</span>
        </span>
        <span className="timestamp">
          on <span className="timestamp-date">{moment(this.props.item.date).format("DD/MM/YYYY")}</span>
        </span>
      </div>
    )
  }
}

export class WasherHistoryItem extends React.Component{
  render(){
    return(
      <div className="history washer">
        <span className="info">
          <Link to={`/History/User/${this.props.item.uid}`} params={{uid:this.props.item.uid}}>{this.props.item.username}</Link>
          <span>changed the detergent of washer ID: </span>
          <Link to={`/History/Washer/${this.props.item.relevantID}`} params={{wid: this.props.item.relevantID}}>
          {this.props.item.relevantID}</Link>
        </span>
        <span className="timestamp">
          on <span className="timestamp-date">{moment(this.props.item.date).format("DD/MM/YYYY")}</span>
        </span>
      </div>
    )
  }
}

export class DryerHistoryItem extends React.Component{
  render(){
    return(
      <div className="history dryer">
        <span className="info">
          <Link to={`/History/User/${this.props.item.uid}`} params={{uid:this.props.item.uid}}>{this.props.item.username}</Link>
          <span>changed the filter of dryer ID: </span>
          <Link to={`/History/Dryer/${this.props.item.relevantID}`} params={{did: this.props.item.relevantID}}>
          {this.props.item.relevantID}</Link>
        </span>
        <span className="timestamp">
          on <span className="timestamp-date">{moment(this.props.item.date).format("DD/MM/YYYY")}</span>
        </span>
      </div>
    )
  }
}

export class ScopeHistoryItem extends React.Component{
  render(){
    return(
      <div className="history scope">
        <span className="info">
          <Link to={`/History/User/${this.props.item.uid}`} params={{uid:this.props.item.uid}}>{this.props.item.username}</Link>
          <span>Updated information of scope: </span>
          <Link to={`History/Scope/${this.props.item.relevantID}`} params={{sid: this.props.item.relevantID}}>
          {this.props.item.relevantID}</Link>
        </span>
        <span className="timestamp">
          on <span className="timestamp-date">{moment(this.props.item.date).format("DD/MM/YYYY")}</span>
        </span>
      </div>
    )
  }
}

export class ScopeDetails extends React.Component{
  render(){
    return(
      <div>
        

      </div>
    )
  }
}

export class Loading extends React.Component {
  state={
    text: "Loading",
  }

  // 1 second tick timer
  tick = () => {
    if(this.state.text === "Loading..."){
      this.setState({
        text: "Loading"
      })
    }else{
      this.setState({
        text: this.state.text + "."
      })
    }
  }

  componentDidMount(){
    this.interval = setInterval(() => this.tick(), 1000);
  }

  render(){
    return(
      <AppPageContainer nopad={true}>
        <div className="loading-graphic">  
          <div className="spinner-border text-dark" role="status">
          </div>
          <span className="sr-only">{this.state.text}</span>
        </div>
      </AppPageContainer>
    )
  }
}

export class PageTransition extends React.Component{
  render(){
    return(
      <div className="page-transition">

      </div>
    )
  }
}

export class AccessDeniedPanel extends React.Component{
  render(){
    return(
      <div className="access-denied-panel">
        <h1 className="header">Access Denied!</h1>
        <p className="body">You do not have permission to access this page.</p>
      </div>
    )
  }
}
// ScopeDetails.proptypes ={
//   scopeID: Proptypes.integer,
//   sampleID: Proptypes.integer,
// }