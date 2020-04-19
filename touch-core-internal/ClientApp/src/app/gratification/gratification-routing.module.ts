import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RewardComponent } from './rewards/reward.component';
import { BadgesComponent } from './badges/badges.component';

const routes: Routes = [
    { path: 'rewards', component: RewardComponent },
    { path: 'badges', component: BadgesComponent },
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class GratificationRoutingModule { }
