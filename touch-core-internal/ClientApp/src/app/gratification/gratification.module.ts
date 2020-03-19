import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BadgesComponent } from './badges/badges.component';
import { RewardComponent } from './rewards/reward.component';
import { RewardFormComponent } from './rewards/reward-form/reward-form.component';
import { GratificationService } from './gratification.service';
import { SharedModule } from '../shared/shared.module';
import { GratificationRoutingModule } from './gratification-routing.module';

@NgModule({
    declarations: [BadgesComponent,
        RewardComponent,
        RewardFormComponent],
    imports: [
        CommonModule,
        SharedModule,
        GratificationRoutingModule
    ],
    providers: [GratificationService]
})
export class GratificationModule { }
