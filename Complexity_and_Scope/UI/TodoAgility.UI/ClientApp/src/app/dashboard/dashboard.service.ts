import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class DashboardService implements Resolve<any>
{
    onFeaturedProjectsChanged: BehaviorSubject<any>;
    onLatestProjectsChanged: BehaviorSubject<any>;
    onProjectActivitiesChanged: BehaviorSubject<any>;
    onActivityDailyCounterChanged: BehaviorSubject<any>;
    onProjectFinishedCounterChanged: BehaviorSubject<any>;
    onActivityFinishedCounterChanged: BehaviorSubject<any>;

    favoritedProjects: any[];
    lastProjects: any[];
    projectActivities: any[];
    activitiesByDayCounter: any[];
    finishedProjectsCounter: any[];
    finishedActivitiesCounter: any[];
    
    constructor(
        private _httpClient: HttpClient
    ){
        this.onFeaturedProjectsChanged = new BehaviorSubject({});
        this.onLatestProjectsChanged = new BehaviorSubject({});
        this.onProjectActivitiesChanged = new BehaviorSubject({});
        this.onActivityDailyCounterChanged = new BehaviorSubject({});
        this.onProjectFinishedCounterChanged = new BehaviorSubject({});
        this.onActivityFinishedCounterChanged = new BehaviorSubject({});

        this.favoritedProjects = [];
        this.lastProjects = [];
        this.projectActivities = [];
        this.activitiesByDayCounter = [];
        this.finishedProjectsCounter = [];
        this.finishedActivitiesCounter = [];
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {
        return new Promise((resolve, reject) => {

            Promise.all([
                this.getFeaturedProjects(), this.getLatestProjects(), this.getActivityByDayCounter(), this.getFinishedProjectsCounter()
            ]).then(
                () => {
                    resolve();
                },
                reject
            );
        });
    }

    loadActivitiesByProject(projectId){
        return new Promise((resolve, reject) => {
            this._httpClient
            .get(`https://localhost:44311/api/Activities/activitiesByProject?ProjectId=${projectId}`)
            .subscribe((response: any) => {
                this.projectActivities = response.items;
                this.onProjectActivitiesChanged.next(this.projectActivities);
                resolve(response);
            });
        });
    }

    getFeaturedProjects(): any {
        return new Promise((resolve, reject) => {
            this._httpClient
            .get('https://localhost:44311/api/projects/featuredProjects')
            .subscribe((response: any) => {
                this.favoritedProjects = response.items;
                this.onFeaturedProjectsChanged.next(this.favoritedProjects);
                resolve(response);
            });
        });
    }

    getLatestProjects(): any {
        return new Promise((resolve, reject) => {
            this._httpClient
            .get('https://localhost:44311/api/projects/lastProjects')
            .subscribe((response: any) => {
                this.lastProjects = response.items;
                this.onLatestProjectsChanged.next(this.lastProjects);
                resolve(response);
            });
        });
    }

    getActivityByDayCounter(): any {
        return new Promise((resolve, reject) => {
            this._httpClient
            .get('https://localhost:44311/api/indicators/activityDailyCounter')
            .subscribe((response: any) => {
                this.activitiesByDayCounter = response;
                this.onActivityDailyCounterChanged.next(this.activitiesByDayCounter);
                resolve(response);
            });
        });
    }

    getFinishedProjectsCounter(): any {
        return new Promise((resolve, reject) => {
            this._httpClient
            .get('https://localhost:44311/api/indicators/projectFinishedCounter')
            .subscribe((response: any) => {
                this.finishedProjectsCounter = response;
                this.onProjectFinishedCounterChanged.next(this.finishedProjectsCounter);
                resolve(response);
            });
        });
    }

    getFinishedActivitiesCounter(): any {
        return new Promise((resolve, reject) => {
            this._httpClient
            .get('https://localhost:44311/api/indicators/activityFinishedCounter')
            .subscribe((response: any) => {
                this.finishedActivitiesCounter = response;
                this.onActivityFinishedCounterChanged.next(this.finishedActivitiesCounter);
                resolve(response);
            });
        });
    }
} 