import React from "react";
import { Loading } from "../../Components/appCommon";
import DatapageLayout from "../PageLayout";

export default class DonorHistory extends React.Component {
  state = {
    content: null,
    headers: [],
    loading: true,
    settings: {},
    error: "",
  };

  settings = {
    title: "DonorHistory",
    primaryColor: "#a6192e",
    accentColor: "#94795d",
    textColor: "#ffffff",
    textColorInvert: "#606060",
    api: "/api/Donations/",
  };

  async componentDidMount() {
    await this.getContent().then((content) => {
      console.log(content);
      this.setState({
        content: content,
      });
    });

    await this.getSettings().then((settings) => {
      console.log(settings);
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
      console.log(res);
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
      console.log(res);
      //Res = {success: true, message: "Success", data: Array(3)}
      return res.json();
    });
  };

  update = async (data) => {
    console.log(data);
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
      console.log(content);
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
        // <DatapageLayout
        //   settings={this.settings}
        //   fieldSettings={this.state.settings.data.FieldSettings}
        //   headers={this.state.settings.data.ColumnSettings}
        //   data={this.state.content.data}
        //   updateHandle={this.handleUpdate}
        //   requestRefresh={this.requestRefresh}
        //   error={this.state.error}
        //   permissions={this.props.permissions}
        //   requestError={this.requestError}
        // ></DatapageLayout>
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
                <span className="tableTitle">Donation History</span>
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
            <th scope="col">Donation Type</th>
            <th scope="col">Donation Amount</th>
            <th scope="col">Donation Date</th>
            <th scope="col">Project Name</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <th scope="row">1</th>
            <td>Online</td>
            <td>$2000</td>
            <td>01/02/2023</td>
            <td>Project Z</td>
          </tr>
          <tr>
            <th scope="row">2</th>
            <td>Online</td>
            <td>$2000</td>
            <td>01/02/2023</td>
            <td>Project Z</td>
          </tr>
          <tr>
            <th scope="row">3</th>
            <td>Online</td>
            <td>$2000</td>
            <td>01/02/2023</td>
            <td>Project Z</td>
          </tr>
          <tr>
            <th scope="row">4</th>
            <td>Online</td>
            <td>$2000</td>
            <td>01/02/2023</td>
            <td>Project Z</td>
          </tr>
          <tr>
            <th scope="row">5</th>
            <td>Online</td>
            <td>$2000</td>
            <td>01/02/2023</td>
            <td>Project Z</td>
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
