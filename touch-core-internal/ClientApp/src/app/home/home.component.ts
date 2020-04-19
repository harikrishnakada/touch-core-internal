import { Component, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts';
import * as moment from 'moment';

import More from 'highcharts/highcharts-more';
More(Highcharts);
import Drilldown from 'highcharts/modules/drilldown';
Drilldown(Highcharts);
// Load the exporting module.
import Exporting from 'highcharts/modules/exporting';
import { sendRequest } from 'selenium-webdriver/http';
import { AuthService } from '../core/services/auth.service';
import { EmployeeService } from '../employee/employee.service';
import { GratificationService } from '../gratification/gratification.service';
import { AttendanceService } from '../attendance/attendance.service';
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
    public badges: any[] = [];

    isLoggedIn: boolean;

    promises: Promise<any>[] = [];

    //Employee Time sheet chart configuation
    public empTimsSheetOptions: any = {
        chart: {
            type: 'column',
            height: 300,
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
            text: '<div><span>Employees Under less working hours </span><div>'
        },
        subtitle: {
            text: '<a href="javascript:undefined" style="color:blue; padding: 30px" class="btn btn-primary" (click)="SendRemainder()"> Send Remainder</a>'
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
            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
            // pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point} days</b><br/>'
        },
    }

    //Employee rewards chart configuation
    public rewardChartOptions: any = {
        chart: {
            type: 'column',
            height: 300
        },
        title: {
            text: 'Employee Rewards'
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            categories: [],
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Number of Rewards Earned'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: []
    };

    constructor(private authService: AuthService, private attendanceService: AttendanceService, private employeeService: EmployeeService, private gratificationService: GratificationService) {
        this.isLoggedIn =  this.authService.authenticated
    }

    ngOnInit() {

        this.promises.push(this.getTimeSheets());
        this.promises.push(this.getEmployees());
        this.promises.push(this.getRewards());
        this.promises.push(this.getBadges());

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

            var rewardsGroupedByEmployees = this.rewards.reduce(function (r, a) {
                r[a.employeeId] = r[a.employeeId] || [];
                r[a.employeeId].push(a);
                return r;
            }, Object.create(null));

            rewardsGroupedByEmployees = Object.keys(rewardsGroupedByEmployees).map(i => rewardsGroupedByEmployees[i]);

            for (let reward of rewardsGroupedByEmployees) {
                this.rewardChartOptions.xAxis.categories.push(`${reward[0]['employee']["name"]}(${reward[0]["employee"]["identifier"]})`);
            }
            for (let badge of this.badges) {
                this.rewardChartOptions.series.push({ "name": badge.badgeName, "id": badge.id, "data": [] });
            }

            for (let ser of this.rewardChartOptions.series) {

                for (let cat of this.rewardChartOptions.xAxis.categories) {
                    var filteredRecs = this.rewards.filter(x => x.badgeId == ser.id && `${x.employee.name}(${x.employee.identifier})` == cat)

                    ser.data.push(filteredRecs.length);
                }
            }

            Highcharts.chart('employee-time-sheet-chart', this.empTimsSheetOptions);
            Highcharts.chart('employee-rewards-chart', this.rewardChartOptions);
        });

    }

    public SendRemainder() {
        console.log("hit");
    }

    getTimeSheets(): Promise<any> {
        return this.attendanceService.getTimeSheetsByHour().toPromise().then((timeSheets) => {
            this.timeSheets = timeSheets;
        })
    }

    getEmployees(): Promise<any> {
        return this.employeeService.GetEmployees().then((employees) => {
            this.employees = employees;
        })
    }

    getRewards(): Promise<any> {
        return this.gratificationService.GetRewards().toPromise().then((rewards) => {
            this.rewards = rewards;
        })
    }
    getBadges(): Promise<any> {
        return this.gratificationService.GetBadges().toPromise().then((badges) => {
            this.badges = badges;
        })
    }
}
