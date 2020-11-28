import { Component, OnInit } from '@angular/core';
import * as Chartist from 'chartist';
import {DashboardService} from '../dashboard/dashboard.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  favoritedProjects: any = [];
  lastProjects: any = [];
  projectActivities: any = [];
  activitiesByDayCounter: any = [];
  finishedProjectsCounter: any = [];
  finishedActivitiesCounter: any = [];

  private _unsubscribeAll: Subject<any>;

  constructor(public dialog: MatDialog, private _dashboardService: DashboardService) {

    this._unsubscribeAll = new Subject();
  }

  close() {
    this.dialog.closeAll();
}

  openDialog() {
    this.dialog.open(DialogContentExampleDialog);
  }

  startAnimationForLineChart(chart){
      let seq: any, delays: any, durations: any;
      seq = 0;
      delays = 80;
      durations = 500;

      chart.on('draw', function(data) {
        if(data.type === 'line' || data.type === 'area') {
          data.element.animate({
            d: {
              begin: 600,
              dur: 700,
              from: data.path.clone().scale(1, 0).translate(0, data.chartRect.height()).stringify(),
              to: data.path.clone().stringify(),
              easing: Chartist.Svg.Easing.easeOutQuint
            }
          });
        } else if(data.type === 'point') {
              seq++;
              data.element.animate({
                opacity: {
                  begin: seq * delays,
                  dur: durations,
                  from: 0,
                  to: 1,
                  easing: 'ease'
                }
              });
          }
      });

      seq = 0;
  };
  startAnimationForBarChart(chart){
      let seq2: any, delays2: any, durations2: any;

      seq2 = 0;
      delays2 = 80;
      durations2 = 500;
      chart.on('draw', function(data) {
        if(data.type === 'bar'){
            seq2++;
            data.element.animate({
              opacity: {
                begin: seq2 * delays2,
                dur: durations2,
                from: 0,
                to: 1,
                easing: 'ease'
              }
            });
        }
      });

      seq2 = 0;
  };

  getActivitiesByProject(projectId){
    this._dashboardService.loadActivitiesByProject(projectId);
  }

  ngOnInit() {

    this._dashboardService.getActivityByDayCounter();
    this._dashboardService.getLatestProjects();
    this._dashboardService.getFeaturedProjects();
    this._dashboardService.getFinishedProjectsCounter();
    this._dashboardService.getFinishedActivitiesCounter();


    this._dashboardService.onFeaturedProjectsChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.favoritedProjects = response;

        if(this.favoritedProjects.length > 0){
          this.getActivitiesByProject(this.favoritedProjects[0].id);
        }
      }
    });

    this._dashboardService.onLatestProjectsChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.lastProjects = response;
      }
    });

    this._dashboardService.onProjectActivitiesChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        this.projectActivities = response;
      }
    });

    this._dashboardService.onActivityDailyCounterChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        const activityDailyCounter: any = {
          labels: response.labels,
          series: response.series
        };
  
        const optionsActivityDailyCounter: any = {
            lineSmooth: Chartist.Interpolation.cardinal({
                tension: 0
            }),
            low: 0,
            high: 50, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
            chartPadding: { top: 0, right: 0, bottom: 0, left: 0},
        }
  
        var activityDailyCounterChart = new Chartist.Line('#activityDailyCounter', activityDailyCounter, optionsActivityDailyCounter);
  
        this.startAnimationForLineChart(activityDailyCounterChart);
      }
    });

    this._dashboardService.onProjectFinishedCounterChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        var dataProjectFinishedCounterChart = {
          labels: response.labels,
          series: response.series
        };
  
        var optionsProjectFinishedCounterChart = {
            axisX: {
                showGrid: false
            },
            low: 0,
            high: 1000,
            chartPadding: { top: 0, right: 5, bottom: 0, left: 0}
        };
        var responsiveOptions: any[] = [
          ['screen and (max-width: 640px)', {
            seriesBarDistance: 5,
            axisX: {
              labelInterpolationFnc: function (value) {
                return value[0];
              }
            }
          }]
        ];
        var projectFinishedCounter = new Chartist.Bar('#projectFinishedCounter', dataProjectFinishedCounterChart, optionsProjectFinishedCounterChart, responsiveOptions);
  
        //start animation for the Emails Subscription Chart
        this.startAnimationForBarChart(projectFinishedCounter);
      }
    });

    this._dashboardService.onActivityFinishedCounterChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(response => {
      if(Object.keys(response).length > 0){
        const dataCompletedTasksChart: any = {
          labels: response.labels,
          series: response.series
        };
  
        const optionsCompletedTasksChart: any = {
          lineSmooth: Chartist.Interpolation.cardinal({
              tension: 0
          }),
          low: 0,
          high: 1000, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
          chartPadding: { top: 0, right: 0, bottom: 0, left: 0}
       }
  
        var completedTasksChart = new Chartist.Line('#completedTasksChart', dataCompletedTasksChart, optionsCompletedTasksChart);
  
        // start animation for the Completed Tasks Chart - Line Chart
        this.startAnimationForLineChart(completedTasksChart);
      }
      
    });
  }

  /**
     * On destroy
     */
    ngOnDestroy(): void {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

}

@Component({
  selector: 'dialog-content-example-dialog',
  templateUrl: './dialog-content-example-dialog.html',
  styleUrls: ['./dialog-content-example-dialog.css']
})
export class DialogContentExampleDialog{

}
