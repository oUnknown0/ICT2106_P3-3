import React from "react";
import { Loading } from "../../Components/appCommon";
import DatapageLayout from "../PageLayout";

export default class DonorAvailableProjects extends React.Component {
  state = {
    content: null,
    headers: [],
    loading: true,
    settings: {},
    error: "",
  };

  settings = {
    title: "DonorAvailableProjects",
    primaryColor: "#a6192e",
    accentColor: "#94795d",
    textColor: "#ffffff",
    textColorInvert: "#606060",
    api: "/api/Project/",
  };

  async componentDidMount() {
    await this.getContent().then((content) => {
      // content.data.FieldSettings.ProjectDonate.displayHeader = "Donate";
      const value = "Donate";
      const keyName = "Donate";
      content.data[0][keyName] = value;
      console.log("Check GET ALL", content);

      console.log(content);
      this.setState({
        content: content,
      });
    });

    await this.getSettings().then((settings) => {
      // change all editable to FALSE since donor shouldnt be able to update project information
      for (const [key, value] of Object.entries(settings.data.FieldSettings)) {
        value.editable = false;
      }

      delete settings.data.ColumnSettings.ProjectId;
      delete settings.data.ColumnSettings.ProjectCompletionDate;
      delete settings.data.ColumnSettings.ProjectStatus;
      delete settings.data.ColumnSettings.ProjectBudget;
      delete settings.data.ColumnSettings.ServiceCenterId;

      delete settings.data.FieldSettings.ProjectId;
      delete settings.data.FieldSettings.ProjectCompletionDate;
      delete settings.data.FieldSettings.ProjectStatus;
      delete settings.data.FieldSettings.ProjectBudget;
      delete settings.data.FieldSettings.ServiceCenterId;

      // rename ProjectDescription displayLabel since theres a typo
      settings.data.FieldSettings.ProjectDescription.displayLabel =
        "Project Description";

      const additionalKeyName = "ProjectDonate";
      settings.data.FieldSettings[additionalKeyName] = {
        displayLabel: "Donation Amount",
        editable: true,
        primaryKey: false,
        toolTip: null,
        type: "number",
      };
      this.setState({
        settings: settings,
      });
    });

    this.setState({
      loading: false,
    });
  }

  getSettings = async () => {
    // fetches http://...:5001/api/User/Settings
    return fetch(this.settings.api + "Settings", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    }).then((res) => {
      // console.log(res);
      return res.json();
    });
  };

  getContent = async () => {
    return fetch(this.settings.api + "All", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    }).then((res) => {
      // console.log(res);
      //Res = {success: true, message: "Success", data: Array(3)}
      return res.json();
    });
  };

  update = async (data) => {
    // console.log(data);
    return fetch(this.settings.api + "UpdateAndFetch/" + data.DonationsId, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    }).then(async (res) => {
      return res.json();
    });
  };

  handleUpdate = async (data) => {
    await this.update(data).then((content) => {
      if (content.success) {
        this.setState({
          error: "",
        });
        return true;
      } else {
        this.setState({
          error: content.message,
        });
        return false;
      }
    });
  };

  requestRefresh = async () => {
    this.setState({
      loading: true,
    });
    await this.getContent().then((content) => {
      // console.log(content);
      this.setState({
        content: content,
        loading: false,
      });
    });
  };

  requestError = async (error) => {
    this.setState({
      error: error,
    });
  };

  render() {
    if (this.state.loading) {
      return <Loading></Loading>;
    } else {
      return (
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
                <span className="tableTitle">Available Projects</span>
            </div>

        </div>
        </div>
        </div>
        </div>

        <div className="row p-3">
        <div className="col-md-12">
        <table class="table table-bordered">
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Project Name</th>
            <th scope="col">Project Description</th>
            <th scope="col">Start Date</th>
            <th scope="col">End Date</th>
            <th scope="col">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <th scope="row">1</th>
            <td>Project X</td>
            <td>Lorem ipsum dolor sit amet, consectetur adipiscing elit, 
              sed do eiusmod tempor incididunt ut labore et dolore magna 
              aliqua. Ut enim ad minim veniam, quis nostrud exercitation 
              ullamco laboris nisi ut aliquip ex ea commodo consequat.</td>
            <td>01/02/2023</td>
            <td>03/03/2023</td>
            <td>
            <a href="/Donate" className="btn btn-primary">Donate</a>
            </td>
          </tr>
          <tr>
            <th scope="row">2</th>
            <td>Project X</td>
            <td>Lorem ipsum dolor sit amet, consectetur adipiscing elit, 
              sed do eiusmod tempor incididunt ut labore et dolore magna 
              aliqua. Ut enim ad minim veniam, quis nostrud exercitation 
              ullamco laboris nisi ut aliquip ex ea commodo consequat.</td>
            <td>01/02/2023</td>
            <td>03/03/2023</td>
            <td>
            <a href="/Donate" className="btn btn-primary">Donate</a>
            </td>
          </tr>
          <tr>
            <th scope="row">3</th>
            <td>Project X</td>
            <td>Lorem ipsum dolor sit amet, consectetur adipiscing elit, 
              sed do eiusmod tempor incididunt ut labore et dolore magna 
              aliqua. Ut enim ad minim veniam, quis nostrud exercitation 
              ullamco laboris nisi ut aliquip ex ea commodo consequat.</td>
            <td>01/02/2023</td>
            <td>03/03/2023</td>
            <td>
            <a href="/Donate" className="btn btn-primary">Donate</a>
            </td>
          </tr>
          <tr>
            <th scope="row">4</th>
            <td>Project X</td>
            <td>Lorem ipsum dolor sit amet, consectetur adipiscing elit, 
              sed do eiusmod tempor incididunt ut labore et dolore magna 
              aliqua. Ut enim ad minim veniam, quis nostrud exercitation 
              ullamco laboris nisi ut aliquip ex ea commodo consequat.</td>
            <td>01/02/2023</td>
            <td>03/03/2023</td>
            <td>
            <a href="/Donate" className="btn btn-primary">Donate</a>
            </td>
          </tr>
          <tr>
            <th scope="row">5</th>
            <td>Project X</td>
            <td>Lorem ipsum dolor sit amet, consectetur adipiscing elit, 
              sed do eiusmod tempor incididunt ut labore et dolore magna 
              aliqua. Ut enim ad minim veniam, quis nostrud exercitation 
              ullamco laboris nisi ut aliquip ex ea commodo consequat.</td>
            <td>01/02/2023</td>
            <td>03/03/2023</td>
            <td>
            <a href="/Donate" className="btn btn-primary">Donate</a>
            </td>
          </tr>
        </tbody>
      </table>
        </div>
        </div>

        </div>
      
      );
    }
  }
}
