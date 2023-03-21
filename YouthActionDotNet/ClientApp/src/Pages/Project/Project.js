import React from "react";
import JsPDF from "jspdf";
import { FaFileWord } from "react-icons/fa";
import { FaFileCsv } from "react-icons/fa";
import { FaFilePdf } from "react-icons/fa";
import { Loading } from "../../Components/appCommon";
import U from "../../Utilities/utilities";
import {
  IconButtonWithText,
  MultiStepBox,
  StdButton,
  IconButton,
} from "../../Components/common";
import { StdInput } from "../../Components/input";
import DatapageLayout from "../PageLayout";
import {
  Cell,
  ListTable,
  HeaderRow,
  ExpandableRow,
} from "../../Components/tableComponents";

import "../../styles/Report.scss";
import { Pie, Bar, Line } from "react-chartjs-2";

export default class Project extends React.Component {
  state = {
    content: null,
    pinned: null,
    archived: null,
    headers: [],
    loading: true,
    settings: {},
    error: "",
    allContent: null,
  };

  settings = {
    title: "Project",
    primaryColor: "#a6192e",
    accentColor: "#94795d",
    textColor: "#ffffff",
    textColorInvert: "#606060",
    api: "/api/Project/",
  };

  async componentDidMount() {
    const perms = await this.props.permissions.find(
      (p) => p.Module === "Project"
    );
    const reformattedPerms = [];
    Object.keys(perms).forEach((perm) => {
      return perm === "Module"
        ? null
        : perms[perm] === true
        ? reformattedPerms.push(perm)
        : null;
    });
    this.setState({
      data: this.props.data,
      perms: perms,
    });

    await this.getContent().then((content) => {
      console.log("here");
      console.log(content);
      this.setState({
        content: content,
      });
    });
    await this.getAllContent().then((allContent) => {
      console.log("here");
      console.log(allContent);
      this.setState({
        allContent: allContent,
      });
    });
    //-------------------------------------------TO BE UPDATED---------------------------------------//
    await this.getPinned().then((pinned) => {
      console.log(pinned);
      this.setState({
        pinned: pinned,
      });
    });
    await this.getArchived().then((archived) => {
      console.log(archived);
      this.setState({
        archived: archived,
      });
    });
    //-------------------------------------------TO BE UPDATED---------------------------------------//
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
  getdate = async () => {
    // fetches http://...:5001/api/User/Settings

    return fetch(this.settings.api + data.ProjectId, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    }).then((res) => {
      console.log(res);
      return res.json();
    });
  };

  getSettings = async () => {
    // fetches http://...:5001/api/User/Settings
    console.log(this.state.pinned.data);
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

  getAllContent = async () => {
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
  getContent = async () => {
    return fetch(this.settings.api + "GetProjectInProgress", {
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
  //-------------------------------------------TO BE UPDATED---------------------------------------//
  getPinned = async () => {
    return fetch(this.settings.api + "GetProjectPinned", {
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

  getArchived = async () => {
    return fetch(this.settings.api + "GetProjectArchived", {
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

  //-------------------------------------------TO BE UPDATED---------------------------------------//

  // getByProjectTag = async () => {
  //     var loggedInVol = this.props.user.data;
  //     console.log(loggedInVol.UserId);
  //     return fetch("/api/Project/GetProjectByTag/testEmployee",{
  //         method: "GET",
  //         headers:{
  //             "Content-Type": "application/json",
  //         },
  //     }).then(response => {
  //         return response.json();
  //     });
  // }

  update = async (data) => {
    console.log(data);
    return fetch(this.settings.api + "UpdateAndFetch/" + data.ProjectId, {
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
  //------------------------------------------------TO BE UPDATED---------------------------------------//
  //pin
  updateStatusToPinned = async (data) => {
    console.log(data);
    return fetch(this.settings.api + "UpdateStatusToPinned/" + data.ProjectId, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    }).then(async (res) => {
      return res.json();
    });
  };
  //pin
  handleUpdateStatusToPinned = async (data) => {
    console.log("Data: " + data);
    await this.updateStatusToPinned(data).then((content) => {
      if (content.success) {
        console.log("Pin success");
        this.setState({
          error: "",
        });
        return true;
      } else {
        console.log("Pin fail");
        this.setState({
          error: content.message,
        });
        return false;
      }
    });
  };

  //archive
  updateStatusToArchive = async (data) => {
    console.log(data);
    return fetch(
      this.settings.api + "UpdateStatusToArchive/" + data.ProjectId,
      {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      }
    ).then(async (res) => {
      return res.json();
    });
  };

  //archive
  handleUpdateStatusToArchive = async (data) => {
    console.log("Data: " + data);
    await this.updateStatusToArchive(data).then((content) => {
      if (content.success) {
        console.log("Archive success");
        this.setState({
          error: "",
        });
        return true;
      } else {
        console.log("Archive fail");
        this.setState({
          error: content.message,
        });
        return false;
      }
    });
  };

  //update to in progress
  updateStatusToInProgress = async (data) => {
    console.log(data);
    return fetch(
      this.settings.api + "UpdateStatusToInProgress/" + data.ProjectId,
      {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      }
    ).then(async (res) => {
      return res.json();
    });
  };

  //update to in progress
  handleUpdateStatusToInProgress = async (data) => {
    console.log("Data: " + data);
    await this.updateStatusToInProgress(data).then((content) => {
      if (content.success) {
        console.log("Unpin success");
        this.setState({
          error: "",
        });
        return true;
      } else {
        console.log("Unpin fail");
        this.setState({
          error: content.message,
        });
        return false;
      }
    });
  };

  requestPinned = async () => {
    this.setState({
      loading: true,
    });
    await this.getPinned().then((pinned) => {
      console.log(pinned);
      this.setState({
        pinned: pinned,
        loading: false,
      });
    });
  };
  requestArchived = async () => {
    this.setState({
      loading: true,
    });
    await this.getArchived().then((archived) => {
      console.log(archived);
      this.setState({
        archived: archived,
        loading: false,
      });
    });
  };
  getChartData1 = (startDate, endDate, budget) => {
    const startMonth = new Date(startDate).getMonth();
    const endMonth = new Date(endDate).getMonth();
    const labels = [];

    if (endMonth - startMonth < 2) {
      const startMonthName = new Date(startDate).toLocaleString("default", {
        month: "short",
      });
      const endMonthName = new Date(endDate).toLocaleString("default", {
        month: "short",
      });
      labels.push(startMonthName, endMonthName);
    } else {
      for (let i = startMonth; i <= endMonth; i++) {
        labels.push(
          new Date(2023, i, 1).toLocaleString("default", { month: "short" })
        );
      }
    }

    const totalMonths = labels.length;
    const data = [];
    let remainingBudget = budget;
    for (let i = 0; i < totalMonths; i++) {
      let estimatedBudget;

      if (totalMonths === 1) {
        estimatedBudget = budget;
      } else if (totalMonths === 2) {
        estimatedBudget = budget / 2;
      } else if (i === 0 || i === totalMonths - 1) {
        estimatedBudget = remainingBudget / 3;
      } else {
        const factor = Math.abs(i - (totalMonths - 1) / 2);
        estimatedBudget =
          (remainingBudget * (factor + 1)) /
          ((totalMonths * (totalMonths + 1)) / 4);
      }

      estimatedBudget = Math.round(estimatedBudget * 100) / 100;

      data.push(estimatedBudget);
      remainingBudget -= estimatedBudget;
    }

    return {
      labels,
      datasets: [
        {
          label: "Estimated Budget",
          data,
          backgroundColor: "#a6192e",
          borderColor: "#a6192e",
          borderWidth: 1,
        },
      ],
    };
  };

  getChartData2 = (startDate, endDate) => {
    const start = new Date(startDate);
    const end = new Date(endDate);
    const today = new Date();
    const daysLeft = Math.ceil((end - today) / (1000 * 60 * 60 * 24));

    const labels = [];
    const data = [];

    let currentDate = start;
    while (currentDate <= end) {
      const formattedDate = currentDate.toLocaleString("en-US", {
        month: "short",
      });
      labels.push(formattedDate);

      if (currentDate <= today) {
        data.push(
          daysLeft - Math.ceil((today - currentDate) / (1000 * 60 * 60 * 24))
        );
      } else {
        data.push(
          daysLeft - Math.ceil((currentDate - today) / (1000 * 60 * 60 * 24))
        );
      }

      currentDate.setMonth(currentDate.getMonth() + 1);
    }

    return {
      labels: labels,
      datasets: [
        {
          label: "daysleft",
          data: data,
          backgroundColor: "#94795d",
        },
      ],
    };
  };

  getChartData3 = (completed, inProgress) => {
    return {
      labels: ["Completed Project", "In Progress Projects"],
      datasets: [
        {
          label: "Projects",
          data: [completed - inProgress, inProgress],
          backgroundColor: ["#5463FF", "#FF1818"],
        },
      ],
    };
  };
  //------------------------------------------------TO BE UPDATED---------------------------------------//
  requestError = async (error) => {
    this.setState({
      error: error,
    });
  };

  render() {
    console.log("Perms project top " + this.state.perms); //check perms
    if (this.state.loading) {
      return <Loading></Loading>;
    } else {
      return (
        <div>
          <DatapageLayout
            settings={this.settings}
            fieldSettings={this.state.settings.data.FieldSettings}
            headers={this.state.settings.data.ColumnSettings}
            data={this.state.content.data}
            updateHandle={this.handleUpdate}
            requestRefresh={this.requestRefresh}
            error={this.state.error}
            permissions={this.props.permissions}
            requestError={this.requestError}
            extraComponents={[
              {
                label: "Archived Projects",
                key: "archivedProjects",
                requiredPerms: ["Create", "Update", "Delete", "Read"],
                component: (
                  <ViewManagement
                    settings={this.settings}
                    perms={this.state.perms}
                    requestRefresh={this.requestArchived}
                    updateHandle={this.handleUpdate}
                    // updateHandle={this.props.updateHandle}
                    headers={this.state.settings.data.ColumnSettings}
                    fieldSettings={this.state.settings.data.FieldSettings}
                    setExpansionContent={this.props.setExpansionContent}
                    data={this.state.archived.data}
                    requestError={this.requestError}
                    api={this.settings.api}
                  >
                    {this.state.archived.data.map((item, index) => {
                      console.log(this.state.content);
                      console.log("Content: " + item + " " + index);
                      console.log(item.ProjectId);
                      return (
                        <div>
                          <br></br>
                          <div>
                            <StdButton
                              onClick={() =>
                                this.handleUpdateStatusToInProgress(item)
                              }
                            >
                              Unarchive Project
                            </StdButton>
                          </div>
                          <br></br>
                          <div>
                            <StdButton
                              onClick={() =>
                                this.handleUpdateStatusToPinned(item)
                              }
                            >
                              Pin Project
                            </StdButton>
                          </div>
                        </div>
                      );
                    })}
                  </ViewManagement>
                ),
              },
              {
                label: "Generate Report PDF",
                key: "generatePDF",
                requiredPerms: ["Create", "Update", "Delete", "Read"],
                component: (
                  <GeneratePDF
                    settings={this.settings}
                    requestRefresh={this.requestRefresh}
                    fieldSettings={this.state.settings.data.FieldSettings}
                    data={this.state.content.data}
                    requestError={this.requestError}
                    api={this.settings.api}
                  ></GeneratePDF>
                ),
              },
              {
                label: "Generate Report CSV",
                key: "generateCSV",
                requiredPerms: ["Create", "Update", "Delete", "Read"],
                component: (
                  <GeneratePDF
                    settings={this.settings}
                    requestRefresh={this.requestRefresh}
                    fieldSettings={this.state.settings.data.FieldSettings}
                    data={this.state.content.data}
                    requestError={this.requestError}
                    api={this.settings.api}
                  ></GeneratePDF>
                ),
              },
              {
                label: "Generate Report Word",
                key: "generateWord",
                requiredPerms: ["Create", "Update", "Delete", "Read"],
                component: (
                  <GeneratePDF
                    settings={this.settings}
                    requestRefresh={this.requestRefresh}
                    fieldSettings={this.state.settings.data.FieldSettings}
                    data={this.state.content.data}
                    requestError={this.requestError}
                    api={this.settings.api}
                  ></GeneratePDF>
                ),
              },
            ]}
          >
            <section id="Pinned">
              <div>
                <h1>Pinned Projects</h1>
              </div>
              <ViewManagement
                settings={this.settings}
                perms={this.state.perms}
                requestRefresh={this.requestPinned}
                updateHandle={this.handleUpdate}
                // updateHandle={this.props.updateHandle}
                headers={this.state.settings.data.ColumnSettings}
                fieldSettings={this.state.settings.data.FieldSettings}
                setExpansionContent={this.props.setExpansionContent}
                data={this.state.pinned.data}
                requestError={this.requestError}
                api={this.settings.api}
              >
                {this.state.pinned.data.map((item, index) => {
                  console.log("Content pinned: " + item + " " + index);
                  console.log(this.state.allContent.data.length);
                  return (
                    <div>
                      <div
                        style={{
                          display: "flex",
                          justifyContent: "space-between",
                        }}
                      >
                        <div
                          style={{ width: 600, height: 300, overflow: "auto" }}
                        >
                          <Bar
                            data={this.getChartData1(
                              item.ProjectStartDate,
                              item.ProjectEndDate,
                              item.ProjectBudget
                            )}
                          />
                        </div>
                        <div
                          style={{ width: 600, height: 300, overflow: "auto" }}
                        >
                          <Line
                            data={this.getChartData2(
                              item.ProjectStartDate,
                              item.ProjectEndDate
                            )}
                          />
                        </div>
                      </div>
                      <br></br>
                      <div>
                        <StdButton
                          onClick={() =>
                            this.handleUpdateStatusToInProgress(item)
                          }
                        >
                          Unpin Project
                        </StdButton>
                      </div>
                      <br></br>
                      <div>
                        <StdButton
                          onClick={() => this.handleUpdateStatusToArchive(item)}
                        >
                          Archive Project
                        </StdButton>
                      </div>
                    </div>
                  );
                })}
              </ViewManagement>
              <br></br>
              <div>
                <h1>Projects</h1>
              </div>
            </section>
            {this.state.content.data.map((item, index) => {
              console.log("Content: " + item + " " + index);
              console.log("here" + item.ProjectEndDate);
              console.log(item.ProjectStartDate);
              return (
                <div className="project-table">
                  <div
                    style={{
                      display: "flex",
                      justifyContent: "space-between",
                    }}
                  >
                    <div style={{ width: 600, height: 300, overflow: "auto" }}>
                      <Bar
                        data={this.getChartData1(
                          item.ProjectStartDate,
                          item.ProjectEndDate,
                          item.ProjectBudget
                        )}
                      />
                    </div>
                    <div style={{ width: 600, height: 300, overflow: "auto" }}>
                      <Line
                        data={this.getChartData2(
                          item.ProjectStartDate,
                          item.ProjectEndDate
                        )}
                      />
                    </div>
                  </div>
                  <br></br>
                  <div>
                    <StdButton
                      onClick={() => this.handleUpdateStatusToPinned(item)}
                    >
                      Pin Project
                    </StdButton>
                  </div>
                  <br></br>
                  <div>
                    <StdButton
                      onClick={() => this.handleUpdateStatusToArchive(item)}
                    >
                      Archive Project
                    </StdButton>
                  </div>
                </div>
              );
            })}
          </DatapageLayout>
        </div>
      );
    }
  }
}
class ViewManagement extends React.Component {
  state = {
    drawerOpen: false,
    expanded: false,
    showBottomMenu: false,
    expansionContent: "",
    expansionComponent: "",
    popUpContent: "",
    data: this.props.data,
    itemsPerPage: 20,
    currentPage: 1,
    pageNumbers: [],
  };
  componentDidMount() {
    // let columns = [];
    // for(var i = 0; i < Object.keys(this.props.fieldSettings).length; i++){
    //     columns.push(
    //         {
    //             label: Object.keys(this.props.fieldSettings)[i],
    //             key: Object.keys(this.props.fieldSettings)[i],
    //         }
    //     );
    // }
    // this.setState({
    //     columns: columns
    // });
  }

  render() {
    if (this.state.content === "") {
      return <div></div>;
    }
    const indexOfLastItem = this.state.currentPage * this.state.itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - this.state.itemsPerPage;
    const currentItems = this.state.data.slice(
      indexOfFirstItem,
      indexOfLastItem
    );
    console.log("Perms project bot " + this.props.perms); //check perms
    console.log("First and last: " + indexOfFirstItem + indexOfLastItem);
    return (
      <div>
        <div className="d-flex justify-content-center align-items-start flex-fill">
          <ListTable settings={this.settings}>
            <HeaderRow>
              {Object.keys(this.props.headers).map((key, index) => {
                return (
                  <Cell width={"100%"} key={index}>
                    {this.props.headers[key].displayHeader}
                  </Cell>
                );
              })}
            </HeaderRow>
            {this.state.data &&
              currentItems.map((row, index) => {
                return (
                  //make 2 charts here

                  <ExpandableRow
                    updateHandle={this.props.updateHandle}
                    values={row}
                    fieldSettings={this.props.fieldSettings}
                    key={index}
                    settings={this.settings}
                    headers={this.props.headers}
                    setExpansionContent={this.setExpansionContent}
                    handleSeeMore={this.handleSeeMore}
                    handleClose={this.handleClose}
                    hasFields={this.props.hasFields}
                    popUpContent={this.state.popUpContent}
                    perms={this.props.perms}
                  >
                    <br></br>
                    <button color="red">Unpin Project</button>
                    <br></br>
                    <br></br>Export to:
                    <FaFileWord size={30} />
                    <FaFilePdf size={30} />
                    <FaFileCsv size={30} />
                    {this.props.children
                      ? this.props.children[
                          index +
                            (this.state.currentPage - 1) *
                              this.state.itemsPerPage
                        ]
                      : ""}
                    {/* <StdButton onClick={() => this.generatePDF()}>Generate PDF</StdButton> */}
                  </ExpandableRow>
                );
              })}
          </ListTable>
        </div>
      </div>
    );
  }
}
ViewManagement.defaultProps = {
  hasFields: true,
};
class GeneratePDF extends React.Component {
  state = {
    columns: [],
    pdfReady: false,
    loading: true,
    content: null,
    inProgress: null,
  };
  settings = {
    title: "Project",
    primaryColor: "#a6192e",
    accentColor: "#94795d",
    textColor: "#ffffff",
    textColorInvert: "#606060",
    api: "/api/Project/",
  };

  async componentDidMount() {
    let columns = [];
    console.log();
    console.log("Generate PDF componentDidMount");
    console.log(this.state.data);
    for (var i = 0; i < Object.keys(this.props.fieldSettings).length; i++) {
      columns.push({
        label: Object.keys(this.props.fieldSettings)[i],
        key: Object.keys(this.props.fieldSettings)[i],
      });
    }
    this.setState({
      columns: columns,
    });
    await this.getAllContent().then((content) => {
      console.log(content);
      this.setState({
        content: content,
      });
    });
    await this.getContent().then((inProgress) => {
      console.log(inProgress);
      this.setState({
        inProgress: inProgress,
      });
    });
  }
  getAllContent = async () => {
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
  getContent = async () => {
    return fetch(this.settings.api + "GetProjectInProgress", {
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
  getChartData3 = (completed, inProgress) => {
    return {
      labels: ["Completed Project", "In Progress Projects"],
      datasets: [
        {
          label: "Projects",
          data: [completed - inProgress, inProgress],
          backgroundColor: ["#FF1818", "#5463FF"],
        },
      ],
    };
  };
  getChartData4 = (projects) => {
    const now = new Date();
    const data = projects.map((project) => {
      const endDate = new Date(project.ProjectEndDate);
      const diffInDays = Math.ceil((endDate - now) / (1000 * 60 * 60 * 24)); // difference in days between now and end date
      return diffInDays;
    });

    const labels = projects.map((project) => project.ProjectName);

    return {
      labels: labels,
      datasets: [
        {
          label: "Days left",
          data: data,
          backgroundColor: ["#5463FF"],
        },
      ],
    };
  };

  reOrderColumns = (index, direction) => {
    var tempColumns = this.state.columns;
    if (direction === "up") {
      if (index > 0) {
        var temp = tempColumns[index];
        tempColumns[index] = tempColumns[index - 1];
        tempColumns[index - 1] = temp;
      }
    } else {
      if (index < tempColumns.length - 1) {
        var temp = tempColumns[index];
        tempColumns[index] = tempColumns[index + 1];
        tempColumns[index + 1] = temp;
      }
    }
    this.setState({
      columns: tempColumns,
    });
  };

  generatePDF = () => {
    this.setState({
      pdfReady: false,
    });

    // Fake loading time to show false sense of progress
    setTimeout(() => {
      this.setState({
        pdfReady: true,
      });
    }, 1000);
  };

  exportPDF = () => {
    const report = new JsPDF("portrait", "pt", "a4");
    report.html(document.getElementById("pdffile")).then(() => {
      report.save(this.props.settings.title + ".pdf");
    });
  };

  render() {
    if (this.state.inProgress == null) {
      return <Loading></Loading>;
    } else {
      return (
        <div id="pdffile" className="container-fluid generate-spreadsheet">
          <section class="report-dashboard">
            <div class="row">
              <div class="flex-container">
                <div class="report-dashboard-item">
                  {console.log(this.state.inProgress.data.ProjectId)}
                  <div
                    style={{
                      width: 600,
                      height: 600,
                      overflow: "auto",
                      margin: "auto",
                    }}
                  >
                    <Pie
                      data={this.getChartData3(
                        this.state.content.data.length,
                        this.state.inProgress.data.length
                      )}
                    />
                  </div>
                </div>
              </div>
            </div>
          </section>
          <section class="report-dashboard">
            <div class="row">
              <div class="flex-container">
                <div class="report-dashboard-item">
                  <div
                    style={{
                      width: 600,
                      height: 600,
                      overflow: "auto",
                      margin: "auto",
                      display: "flex",
                      alignItems: "center",
                      justifyContent: "center",
                    }}
                  >
                    <Bar
                      data={this.getChartData4(this.state.inProgress.data)}
                    />
                  </div>
                </div>
              </div>
            </div>
          </section>
          <section class="report-dashboard">
            <div class="row">
              <div class="flex-container">
                <div class="report-dashboard-item">
                  <div class="report-dashboard-label">Project name</div>
                  <div class="report-dashboard-number">
                    deaededb-1788-44bd-b0d8-96a01368156e
                  </div>
                </div>
              </div>
            </div>
          </section>
          <section class="report-dashboard">
            <div class="row">
              <div class="flex-container">
                <div class="report-dashboard-item">
                  <div class="report-dashboard-label">Project name</div>
                  <div class="report-dashboard-number">Test project</div>
                </div>
              </div>
            </div>
          </section>
          <section class="report-dashboard">
            <div class="row">
              <div class="flex-container">
                <div class="report-dashboard-item">
                  <div class="report-dashboard-label">PROJECT DESCRIPTION</div>
                  <div class="report-dashboard-number">
                    testProject description
                  </div>
                </div>
              </div>
            </div>
          </section>
          <section class="report-dashboard">
            <div class="row">
              <div class="flex-container">
                <div class="report-dashboard-item">
                  <div class="report-dashboard-label">PROJECT START DATE</div>

                  <div class="report-dashboard-number">2023-01-19T00:45</div>
                </div>
              </div>
            </div>
          </section>
          <section class="report-dashboard">
            <div class="row">
              <div class="flex-container">
                <div class="report-dashboard-item">
                  <div class="report-dashboard-label">
                    PROJECT COMPLETION DATE
                  </div>
                  <div class="report-dashboard-number">2023-01-19T00:46</div>
                </div>
              </div>
            </div>
          </section>
          <section class="report-dashboard">
            <div class="row">
              <div class="flex-container">
                <div class="report-dashboard-item">
                  <div class="report-dashboard-label">PROJECT STATUS</div>
                  <div class="report-dashboard-number">In progress</div>
                </div>
              </div>
            </div>
          </section>
          <section class="report-dashboard">
            <div class="row">
              <div class="flex-container">
                <div class="report-dashboard-item">
                  <div class="report-dashboard-label">PROJECT BUDGET</div>
                  <div class="report-dashboard-number">1000000</div>
                </div>
              </div>
            </div>
          </section>
          <section class="report-dashboard">
            <div class="row">
              <div class="flex-container">
                <div class="report-dashboard-item">
                  <div class="report-dashboard-label">SERVICE CENTER ID</div>
                  <div class="report-dashboard-number">
                    Test Employee Center
                  </div>
                </div>
              </div>
            </div>
          </section>
          {}
          <div className="generate-actions">
            <StdButton onClick={() => this.generatePDF()}>
              Generate PDF
            </StdButton>

            {this.state.pdfReady ? (
              // <CSVLink data={this.props.data} className={"forget-password"} headers={this.state.columns} filename={this.props.settings.title + ".csv"}>Download</CSVLink>
              <button onClick={this.exportPDF} type="button">
                Export PDF
              </button>
            ) : (
              <div className="spinner-border" role="status">
                <span className="visually-hidden">Loading...</span>
              </div>
            )}
          </div>
        </div>
      );
    }
  }
}
