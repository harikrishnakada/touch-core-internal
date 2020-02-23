import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import * as Highcharts from 'highcharts';
import { TimeSheetService } from '../services/time-sheet.service';
import * as moment from 'moment';

import More from 'highcharts/highcharts-more';
More(Highcharts);
import Drilldown from 'highcharts/modules/drilldown';
Drilldown(Highcharts);
// Load the exporting module.
import Exporting from 'highcharts/modules/exporting';
import { sendRequest } from 'selenium-webdriver/http';
import { EmployeeService } from '../services/employee.service';
import { GratificationService } from '../services/gratification.service';
// Initialize exporting module.
Exporting(Highcharts);

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    public timeSheets: any[] = [];
    public employees: any[] = [];
    public rewards: any[] = [];

    promises: Promise<any>[] = [];

    public empTimsSheetOptions: any = {
        chart: {
            type: 'column',
            events: {
                drilldown: function (e) {
                    // this.xAxis[0].setTitle({ text: "down" });
                    this.yAxis[0].setTitle({ text: "Number of hours" });

                },
                drillup: function (e) {
                    this.yAxis[0].setTitle({ text: "Number of days" });
                }

            }
        },
        title: {
            text: 'Employees Under less working hours'
        },
        subtitle: {
            text: ''
        },
        accessibility: {
            announceNewData: {
                enabled: true
            }
        },
        xAxis: {
            type: 'category',
            title: {
                text: 'Employees'
            }
        },
        yAxis: {
            title: {
                text: 'Number of days'
            }

        },
        legend: {
            enabled: false
        },
        plotOptions: {
            series: {
                borderWidth: 0,
                dataLabels: {
                    enabled: true
                    //  format: '{point.y:.1f}%'
                }
            }
        },



        series: [
            {
                name: "Employee",
                colorByPoint: true,
                data: []
            }
        ],
        drilldown: {

            series: [{
                tooltip: {
                    //Pointformat for the percentage series
                    pointFormat: 'FUCK'
                },
            }
            ]
        },
        tooltip: {
            //pointFormatter: function () {
            //    if (this.hasOwnProperty("drilldown")) {
            //        return "<b>{series.name]:({point.y}) parent</b>";
            //    } else {
            //        return `<span style="color: { point.color }">{point.name}</span>: <b>{point.y} days</b><br/>`;
            //    }
            //},
            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
            // pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point} days</b><br/>'
        },
    }

    constructor(private authService: AuthService, private timeSheetService: TimeSheetService, private employeeService: EmployeeService, private rewardsService: GratificationService) {

    }

    ngOnInit() {

        this.promises.push(this.getTimeSheets());
        this.promises.push(this.getEmployees());
        this.promises.push(this.getRewards());

        Promise.all(this.promises).then(() => {

            var timeSheetsGroupedByEmployees = this.timeSheets.reduce(function (r, a) {
                r[a.employeeId] = r[a.employeeId] || [];
                r[a.employeeId].push(a);
                return r;
            }, Object.create(null));

            timeSheetsGroupedByEmployees = Object.keys(timeSheetsGroupedByEmployees).map(i => timeSheetsGroupedByEmployees[i]);

            for (let timeSheets of timeSheetsGroupedByEmployees) {


                this.empTimsSheetOptions.series[0].data.push({
                    "name": `${timeSheets[0]['employee']["name"]}(${timeSheets[0]["employee"]["identifier"]})`,
                    "drilldown": `${timeSheets[0]['employee']["name"]}(${timeSheets[0]["employee"]["identifier"]})`,
                    "y": timeSheets.length
                })

                var series = {};
                series["name"] = `${timeSheets[0]['employee']["name"]}(${timeSheets[0]["employee"]["identifier"]})`;
                series["id"] = `${timeSheets[0]['employee']["name"]}(${timeSheets[0]["employee"]["identifier"]})`;
                series["data"] = [];

                for (let timeSheet of timeSheets) {
                    //series["name"] = timeSheet.employee.name;
                    //series["id"] = timeSheet.employee.name;
                    series["data"].push([timeSheet["fromDateTime"], timeSheet["hours"]])
                }
                this.empTimsSheetOptions.drilldown.series.push(series);
            }

            Highcharts.chart('employee-time-sheet-chart', this.empTimsSheetOptions);
        });

    }

    getTimeSheets(): Promise<any> {
        return this.timeSheetService.GetTimeSheets().then((timeSheets) => {
            this.timeSheets = timeSheets;
        })
    }

    getEmployees(): Promise<any> {
        return this.employeeService.GetEmployees().then((employees) => {
            this.employees = employees;
        })
    }

    getRewards(): Promise<any> {
        return this.rewardsService.GetRewards().then((rewards) => {
            this.rewards = rewards;
        })
    }
}
