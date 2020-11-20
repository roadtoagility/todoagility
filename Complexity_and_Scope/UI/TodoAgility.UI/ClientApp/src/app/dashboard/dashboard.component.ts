import { Component, OnInit } from '@angular/core';
import * as Chartist from 'chartist';
import {DashboardService} from '../dashboard/dashboard.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  favoritedProjects: any[] = [];
  lastProjects: any[] = [];
  projectActivities: any[] = [];
  activitiesByDayCounter: any[] = [];
  finishedProjectsCounter: any[] = [];
  finishedActivitiesCounter: any[] = [];

  private _unsubscribeAll: Subject<any>;

  constructor(private _dashboardService: DashboardService) {

    this._unsubscribeAll = new Subject();

    this.lastProjects.push({
      projectId: 1,
      name: "FaturamentoWeb",
      budget: 36.738,
      client: "Potencial"
    },
    {
      projectId: 2,
      name: "SCA",
      budget: 23.789,
      client: "Localiza"
    },{
      projectId: 3,
      name: "API Rodovias",
      budget: 6.142,
      client: "Fiat"
    },
    {
      projectId: 4,
      name: "CargasWeb",
      budget: 38.200,
      client: "ANTT"
    });

    this.favoritedProjects.push({
      name: "SCA",
      icon: "bug_report",
      id: 1
    },
    {
      name: "FaturamentoWeb",
      icon: "code",
      id: 2
    },
    {
      name: "API Rodovias",
      icon: "cloud",
      id: 3
    });

    this.projectActivities.push({
      projectId: 1,
      activities: [
      {
        id: 1,
        title: "Desenho da interface de inclusão de usuários"
      },
      {
        id: 1,
        title: "Criação do PDM de modelo do banco"
      },
      {
        id: 1,
        title: "Integração com Active Directory"
      },
      {
        id: 1,
        title: "Criar o board para SPRINT 5 com os épicos e estórias envolvidas, alinhar com equipe"
      }]
    });
    
    this.projectActivities.push({
      projectId: 2,
      activities: [
      {
        id: 1,
        title: "Desenho da interface de inclusão de usuários"
      },
      {
        id: 1,
        title: "Criação do PDM de modelo do banco"
      }]
    });

    this.projectActivities.push({
      projectId: 3,
      activities: [
      {
        id: 1,
        title: "Desenho da interface de inclusão de usuários"
      }]
    });  
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
  ngOnInit() {

    this._dashboardService.getActivityByDayCounter();
    this._dashboardService.getLatestProjects();
    this._dashboardService.getFavoritedProjects();
    this._dashboardService.getFinishedProjectsCounter();
    this._dashboardService.getFinishedActivitiesCounter();


    this._dashboardService.onProjectActivitiesChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(entidades => {
                console.log(entidades);
            });

      /* ----------==========     Daily Sales Chart initialization For Documentation    ==========---------- */

      const dataDailySalesChart: any = {
          labels: ['M', 'T', 'W', 'T', 'F', 'S', 'S'],
          series: [
              [12, 17, 7, 17, 23, 18, 38]
          ]
      };

     const optionsDailySalesChart: any = {
          lineSmooth: Chartist.Interpolation.cardinal({
              tension: 0
          }),
          low: 0,
          high: 50, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
          chartPadding: { top: 0, right: 0, bottom: 0, left: 0},
      }

      var dailySalesChart = new Chartist.Line('#dailySalesChart', dataDailySalesChart, optionsDailySalesChart);

      this.startAnimationForLineChart(dailySalesChart);


      /* ----------==========     Completed Tasks Chart initialization    ==========---------- */

      const dataCompletedTasksChart: any = {
          labels: ['12p', '3p', '6p', '9p', '12p', '3a', '6a', '9a'],
          series: [
              [230, 750, 450, 300, 280, 240, 200, 190]
          ]
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



      /* ----------==========     Emails Subscription Chart initialization    ==========---------- */

      var datawebsiteViewsChart = {
        labels: ['J', 'F', 'M', 'A', 'M', 'J', 'J', 'A', 'S', 'O', 'N', 'D'],
        series: [
          [542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 895]

        ]
      };
      var optionswebsiteViewsChart = {
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
      var websiteViewsChart = new Chartist.Bar('#websiteViewsChart', datawebsiteViewsChart, optionswebsiteViewsChart, responsiveOptions);

      //start animation for the Emails Subscription Chart
      this.startAnimationForBarChart(websiteViewsChart);
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
