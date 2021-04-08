<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png" />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import * as Highcharts from "highcharts";
import * as repository from "@/repository";
import { ChartData } from "@/domain";
@Component({
  components: {},
})
export default class DailyList extends Vue {
  chartData: ChartData = null;

  graph() {
    Highcharts.chart({
      chart: {
        type: "line",
        renderTo: "graph",
      },
      title: false as Highcharts.TitleOptions,
      xAxis: {
        categories: this.chartData.categories,
        crosshair: true,
      } as Highcharts.XAxisOptions,
      yAxis: {
        title: false,
        labels: {
          format: "{value}",
        },
        opposite: false,
      } as Highcharts.YAxisOptions,
      series: this.chartData.data,
    });
  }
}
</script>
