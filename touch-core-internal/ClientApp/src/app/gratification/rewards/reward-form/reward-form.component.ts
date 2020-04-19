import { Component, OnInit, ViewChild, Output, EventEmitter, ElementRef, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgSelectModule, NgOption } from '@ng-select/ng-select';
import { NgForm } from '@angular/forms';
import { environment } from '../../../../environments/environment';
import { GratificationService } from '../../gratification.service';


@Component({
    selector: 'app-reward-form',
    templateUrl: './reward-form.component.html',
    styleUrls: ['./reward-form.component.css']
})
export class RewardFormComponent implements OnInit {
    @ViewChild('addReward', { static: false }) addRewardForm: NgForm;
    @Output() rewardAdded = new EventEmitter<any>();
    @ViewChild("myDiv", { static: false }) divView: ElementRef;

    employees: any = [];
    badges: any = [];
    newReward: any = {};
    promises: Promise<any>[] = [];

    constructor(private http: HttpClient, private SpinnerService: NgxSpinnerService,
         private gService: GratificationService) { }

    ngOnInit() {
        this.SpinnerService.show();
        this.promises.push(this.GetEmployees());
        this.promises.push(this.GetBadges());

        Promise.all(this.promises).then(() => {
            this.SpinnerService.hide();
        });

    }

    async GetEmployees(): Promise<any> {
        return this.gService.GetEmployees().toPromise().then((result) => {
            this.employees = result;
            console.log(result);
        });
    }

    async GetBadges(): Promise<any> {
        return this.gService.GetBadges().toPromise().then((result) => {
            this.badges = result;
        });
    }

    onSubmit(form: NgForm) {
        console.log(form);
        this.SpinnerService.show();
        //this.toogleModal("#addRewardModal");
        this.http.post(environment.baseUrl + "/reward", this.newReward).toPromise().then(() => {
            this.reset();
            this.rewardAdded.emit(form.value);
            this.SpinnerService.hide();
        });

    }

    reset() {
        this.addRewardForm.reset();
    }

    toogleModal(selector: string) {
        (<any>$(selector)).modal('toogle');
    }

}
