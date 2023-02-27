import Nav, {  } from './Components/nav'
import { Routes, Route, useNavigate } from 'react-router-dom';
import React, { useEffect } from 'react';
import Login from './Pages/Login';
import Register from './Pages/Register';
import useToken from './Components/useToken';
import usePerms from './Components/usePerms';
import Logout from './Pages/Logout';
import SlideDrawer, { Backdrop, DrawerItem, DrawerSection } from './Components/sideNav';

import homeImg from "./Assets/nav/house.png";
import userImg from "./Assets/nav/user.png";

import logoutImg from "./Assets/nav/logout.png";

import Users from './Pages/Users';
import Employees from './Pages/Employee/Employees';
import Volunteer from './Pages/Volunteer/Volunteer';
import VolunteerRegistration from './Pages/Volunteer/VolunteerRegistration';
import Donors from './Pages/Donor/Donor';
import Donations from './Pages/Donor/Donations';
import DonorDashboard from './Pages/Donor/DonorDashboard';
import Donate from './Pages/Donor/Donate';
import DonorHistory from './Pages/Donor/DonorHistory';
import DonorAvailableProjects from './Pages/Donor/DonorAvailableProjects';
import ServiceCenters from './Pages/Employee/ServiceCenters';
import VolunteerWork from './Pages/Volunteer/VolunteerWork';
import Project from './Pages/Project/Project';
import Expense from './Pages/Expense/Expense';
import Permissions from './Pages/Admin/Permissions';

import Home  from "./Pages/Home"
import VolunteerHome from "./Pages/Volunteer/volunteerHome"

import Sample from './Pages/Sample';


/* function getToken() {  
  const tokenString = sessionStorage.getItem('token');
  const userToken = JSON.parse(tokenString);
  return userToken?.token
} */

export default function App() {
  const { token, setToken, logout } = useToken();
  const { perms, setPerms, clearPerms} = usePerms();
  const [drawerOpen, setDrawerOpen] = React.useState(false);
  const [active, setActive] = React.useState("Dashboard")
  const [height, setHeight] = React.useState(window.innerHeight);

  const drawerToggleClickHandler = () => {
    setDrawerOpen(!drawerOpen)
  }

  useEffect(()=>{
    window.addEventListener('resize', () => {
      setHeight(window.innerHeight);
    });
  })
  
  let backdrop;
  if (drawerOpen) {
    backdrop = <Backdrop toggle={drawerToggleClickHandler} />;
  }

  const navigate = useNavigate();
  if (!token) {
    return (
        <div className="App" style={{maxHeight: height}}>
          <LoggedOutNav toggle={drawerToggleClickHandler}></LoggedOutNav>
          <div className="App-header" style={{maxHeight: height - 56}}>
            <SlideDrawer show={drawerOpen} toggle={drawerToggleClickHandler} direction="top">
              <DrawerSection label={"Modules"}>
                <DrawerItem label="Dashboard" to={"/"} logo={homeImg}></DrawerItem>
                <DrawerItem label="Volunteer Registration" to={"volunteer-registration"} logo={homeImg}></DrawerItem>
              </DrawerSection>
            </SlideDrawer>
            {backdrop}
            <Routes>
              <Route path="/" element={
                <Login setToken={setToken} setPerms={setPerms}></Login>}></Route>
              <Route path="/Register" element={<Register />}></Route>
              <Route path="/volunteer-registration" element={<VolunteerRegistration setToken={setToken} setPerms={setPerms}/>}></Route>
            </Routes>
          </div>
        </div>
    )
  } else {
    const parsedPerms = JSON.parse(perms);
    return (
        <div className="App" style={{maxHeight: height}}>
          <LoggedInNav user={token.data} logout={logout} toggle={drawerToggleClickHandler} show={drawerOpen}></LoggedInNav>
          <header className="App-header" style={{maxHeight: height - 56}}>
            {backdrop}
            <SlideDrawer show={drawerOpen} toggle={drawerToggleClickHandler} direction={"top"}>
              
              <DrawerSection label={"Modules"}>
                <DrawerItem label="Home" to={"/"} logo={userImg} currentActive = {active} setActive={setActive}></DrawerItem>
                
                {parsedPerms.map((perm)=>{<DrawerItem label="Employees" to={"/Employees"} logo={userImg} currentActive = {active} setActive={setActive}></DrawerItem>
                const currMod = perm.Module;
                const toTextUrl = perm.Module.replace(" ", "-");
                if(perm.Read){
                  return <DrawerItem label={currMod} to={"/" + toTextUrl} logo={userImg}></DrawerItem>
                }
                })}
                <DrawerItem label="Logout" to={"/Logout"} logo={logoutImg}></DrawerItem>
              </DrawerSection>
            </SlideDrawer>
            
            <Routes>
              {token.data.Role == "Admin" &&
                <Route path="/" element={<Home user={token} permissions = {parsedPerms}/>}/>
              }
              {token.data.Role == "Donor" &&
                <Route path="/" element={<DonorDashboard user={token} permissions = {parsedPerms}/>}/>
             
              }
              {token.data.Role == "Volunteer" &&
                <Route path="/" element={<VolunteerHome user={token} permissions = {parsedPerms}/>}/>
              }
              {token.data.Role == "Employee" ?
                
              token.data.EmployeeRole == "Regional Director" ?
                <Route path="/" element={<Home user={token} permissions = {parsedPerms}/>}/>
              :
              token.data.EmployeeRole == "Service Center Manager" ?
                <Route path="/" element={<Home user={token} permissions = {parsedPerms}/>}/>
              :
              token.data.EmployeeRole == "Chief Executive" ?
                <Route path="/" element={<Home user={token} permissions = {parsedPerms}/>}/>
              :
              token.data.EmployeeRole == "Finance Director" ?
                <Route path="/" element={<Home user={token} permissions = {parsedPerms}/>}/>
              :
              token.data.EmployeeRole == "Operating Manager" ?
                <Route path="/" element={<Home user={token} permissions = {parsedPerms}/>}/>
              :
              token.data.EmployeeRole == "Marketing Manager" ?
                <Route path="/" element={<Home user={token} permissions = {parsedPerms}/>}/>
              :
              token.data.EmployeeRole == "Director Of New Business" ?
                <Route path="/" element={<Home user={token} permissions = {parsedPerms}/>}/>
              :
              ""
              :
              ""
              }
              <Route path="/Users" element={<Users user={token} permissions = {parsedPerms}/>}/>  
              <Route path="/Employees" element={<Employees user={token} permissions = {parsedPerms}/>}/>
              <Route path="/Volunteers" element={<Volunteer user={token} permissions = {parsedPerms}/>}/>
              <Route path="/volunteer-Registration" element={<VolunteerRegistration user={token} permissions = {parsedPerms}/>}/>
              <Route path="/Donors" element={<Donors user={token} permissions = {parsedPerms}></Donors>}/>
              <Route path="/Donations" element={<Donations user={token} permissions = {parsedPerms}></Donations>}/>
              <Route path="/DonorDashboard" element={<DonorDashboard user={token} permissions = {parsedPerms}></DonorDashboard>}/>
              <Route path="/DonorHistory" element={<DonorHistory user={token} permissions = {parsedPerms}></DonorHistory>}/>
              <Route path="/DonorAvailableProjects" element={<DonorAvailableProjects user={token} permissions = {parsedPerms}></DonorAvailableProjects>}/>
              <Route path="/Donate" element={<Donate user={token} permissions = {parsedPerms}></Donate>}/>
              <Route path="/Volunteer-Work" element={<VolunteerWork user={token} permissions = {parsedPerms}/>}/>
              <Route path="/Service-Center" element={<ServiceCenters user={token} permissions = {parsedPerms}/>}/>
              <Route path="/Project" element={<Project user={token} permissions = {parsedPerms}/>}/>
              <Route path="/Expenses" element={<Expense user={token} permissions = {parsedPerms}/>}/>
              <Route path="/Permissions" element={<Permissions user={token} permissions = {parsedPerms}/>}/>
              <Route path="/Sample" element={<Sample user={token} permissions = {parsedPerms}/>}/>
              <Route path="/Logout" element={<Logout logout={logout} clearPerms={clearPerms}></Logout>}/>

            </Routes>
          </header>
        </div>
    );
  }
}


class LoggedInNav extends React.Component {

  state = {
    title: "YouthAction"
  }
  componentDidMount() {
    window.addEventListener("resize", this.resize.bind(this));
    this.resize();
  }

  resize() {
    const md = 768;
   if(window.innerWidth >= md){
        this.setState({
          title: "YouthAction"
        })
    } else{
        this.setState({
          title: "YouthAction"
        })
    }
  }
  render() {

    return (
      <Nav user={this.props.user} title={this.state.title
      } toggle={this.props.toggle} show={this.props.show}>
      </Nav>
    )
  }
}

class LoggedOutNav extends React.Component {
  render() {
    return (
      <Nav title={
        "YouthAction"
      } toggle={this.props.toggle} >
      </Nav>
    )
  }
}