import React from "react";
import { Loading } from "../../Components/appCommon";
import DatapageLayout from "../PageLayout";
import "../../styles/donorDashboard.css";
import { Card, CardBody, CardTitle, CardSubtitle } from "reactstrap";
import {
  FaMoneyBillWave,
  FaTrophy,
  FaChartLine,
  FaProjectDiagram,
} from "react-icons/fa";

import Chart from "chart.js/auto";
import { Bar } from "react-chartjs-2";

const TestBarChart = () => {
  const labels = ["January", "February", "March", "April", "May", "June"];
  const data = {
    labels: labels,
    datasets: [
      {
        label: "My First dataset",
        backgroundColor: "rgb(255, 99, 132)",
        borderColor: "rgb(255, 99, 132)",
        data: [0, 10, 5, 2, 20, 30, 45],
      },
    ],
  };
  console.log("TestBarChart");
  return (
    <div className="row">
      <Bar data={data} />
    </div>
  );
};
export default class DonorDashboard extends React.Component {
  state = {
    content: null,
    headers: [],
    loading: true,
    settings: {},
    error: "",
  };

  settings = {
    title: "DonorDashboard",
    primaryColor: "#a6192e",
    accentColor: "#94795d",
    textColor: "#ffffff",
    textColorInvert: "#606060",
    api: "/api/Donor/",
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
    return fetch(this.settings.api + "UpdateAndFetch/" + data.UserId, {
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
        <div className="col-md-12">
          <div className="row">
            <div className="tableHeader p-4">
              <div className="tableHeaderActions ">
                <div className="d-flex justify-content-start align-items-center">
                  <div className="tableTitleContainer">
                    <div
                      className="tableTitlePulseAnimation-1"
                      style={
                        this.state.searchBarExtended
                          ? { "--ScaleMultiplier": 0.75 }
                          : { "--ScaleMultiplier": 2 }
                      }
                    ></div>
                    <div
                      className="tableTitlePulseAnimation-2"
                      style={
                        this.state.searchBarExtended
                          ? { "--ScaleMultiplier": 0.75 }
                          : { "--ScaleMultiplier": 2 }
                      }
                    ></div>
                    <div
                      className="tableTitlePulseAnimation-3"
                      style={
                        this.state.searchBarExtended
                          ? { "--ScaleMultiplier": 0.75 }
                          : { "--ScaleMultiplier": 2 }
                      }
                    ></div>
                    <span className="tableTitle">Donor Dashboard</span>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div className="card-group mt-3">
            <Card className="card-style">
              <CardBody>
                <FaMoneyBillWave className="mr-2 icon-style" />
                <CardTitle className="card-title">$100 000</CardTitle>
                <CardSubtitle className="card-subtitle">
                  Total Donations
                </CardSubtitle>
              </CardBody>
            </Card>
            <Card className="card-style">
              <CardBody>
                <FaTrophy className="mr-2 icon-style" />
                <CardTitle className="card-title">$10 000</CardTitle>
                <CardSubtitle className="card-subtitle">
                  Highest Donation
                </CardSubtitle>
              </CardBody>
            </Card>
            <Card className="card-style">
              <CardBody>
                <FaChartLine className="mr-2 icon-style" />
                <CardTitle className="card-title">$50</CardTitle>
                <CardSubtitle className="card-subtitle">
                  Average Donation
                </CardSubtitle>
              </CardBody>
            </Card>
            <Card className="card-style">
              <CardBody>
                <FaProjectDiagram className="mr-2 icon-style" />
                <CardTitle className="card-title">#5</CardTitle>
                <CardSubtitle className="card-subtitle">
                  Number of Projects Donated
                </CardSubtitle>
              </CardBody>
            </Card>
          </div>

          <div className="row justify-content-center p-3">
            <div className="col-md-7 mt-4">
              <Card>
                <CardBody>
                  <h3 className="text-start p-5">Donation Analysis</h3>
                  {/* <img
                    className="card-img"
                    src={BarChart}
                    height={420}
                    alt="Card image"
                  ></img> */}
                  <TestBarChart />
                </CardBody>
              </Card>
            </div>

            <div className="col-md-5 mt-4">
              <Card className="p-5">
                <CardBody>
                  <div className="d-flex justify-content-between mb-5">
                    <h3>Available Projects</h3>
                    <a href="/DonorAvailableProjects" className="view-all">
                      View All
                    </a>
                  </div>
                  {/*project name and project description and button (center align) align */}
                  <div className="d-flex justify-content-between mb-5">
                    <div className="d-flex flex-column">
                      <h5>Project 1</h5>
                      <p className="text-muted">
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                        sed do eiusmod tempor incididunt ut labore et dolore
                        magna aliqua. Ut enim ad minim veniam, quis nostrud
                        exercitation ullamco laboris nisi ut aliquip ex ea
                        commodo consequat.
                      </p>
                    </div>
                    <a href="/DonorProjectDetails" className="btn view-details">
                      View
                    </a>
                  </div>

                  <div className="d-flex justify-content-between mb-5">
                    <div className="d-flex flex-column">
                      <h5>Project 1</h5>
                      <p className="text-muted">
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                        sed do eiusmod tempor incididunt ut labore et dolore
                        magna aliqua. Ut enim ad minim veniam, quis nostrud
                        exercitation ullamco laboris nisi ut aliquip ex ea
                        commodo consequat.
                      </p>
                    </div>
                    <a href="/DonorProjectDetails" className="btn view-details">
                      View
                    </a>
                  </div>

                  <div className="d-flex justify-content-between mb-5">
                    <div className="d-flex flex-column">
                      <h5>Project 1</h5>
                      <p className="text-muted">
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                        sed do eiusmod tempor incididunt ut labore et dolore
                        magna aliqua. Ut enim ad minim veniam, quis nostrud
                        exercitation ullamco laboris nisi ut aliquip ex ea
                        commodo consequat.
                      </p>
                    </div>
                    <a href="/DonorProjectDetails" className="btn view-details">
                      View
                    </a>
                  </div>
                </CardBody>
              </Card>
            </div>
          </div>

          <div className="row justify-content-center">
            <div className="col-md-12 p-4">
              <div className="card p-5">
                <div className="d-flex justify-content-between">
                  <h3>Donation History</h3>
                  <a href="/DonorHistory" class="view-all">
                    View All
                  </a>
                </div>
                <div className="card-body">
                  <table className="table">
                    <thead>
                      <tr>
                        <th scope="col">#</th>
                        <th scope="col">Name</th>
                        <th scope="col">Type</th>
                        <th scope="col">Date</th>
                        <th scope="col">Amount</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr>
                        <th scope="row">1</th>
                        <td>Project X</td>
                        <td>Online</td>
                        <td>06/02/2023</td>
                        <td>$2000</td>
                      </tr>
                      <tr>
                        <th scope="row">2</th>
                        <td>Project B</td>
                        <td>Cash</td>
                        <td>06/02/2023</td>
                        <td>$2000</td>
                      </tr>
                      <tr>
                        <th scope="row">3</th>
                        <td>Project A</td>
                        <td>Online</td>
                        <td>06/02/2023</td>
                        <td>$2000</td>
                      </tr>
                      <tr>
                        <th scope="row">4</th>
                        <td>Project A</td>
                        <td>Online</td>
                        <td>06/02/2023</td>
                        <td>$2000</td>
                      </tr>
                      <tr>
                        <th scope="row">5</th>
                        <td>Project A</td>
                        <td>Online</td>
                        <td>06/02/2023</td>
                        <td>$2000</td>
                      </tr>
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
        </div>
      );
    }
  }
}
