document.addEventListener("DOMContentLoaded", function () {
  
    // -----------------------------------------------------------------------
    // breacrumb chart
    // -----------------------------------------------------------------------
    var breadbar = {
      series: [
        {
          name: "",
          data: [5, 8, 7, 12, 6, 7, 15,20],
        },
      ],
      chart: {
        type: "bar",
        width: 70,
        height: 40,
        toolbar: {
          show: false,
        },
        sparkline: {
          enabled: true,
        },
      },
      colors: ["var(--bs-primary)"],
      grid: {
        show: false,
      },
      plotOptions: {
        bar: {
          horizontal: false,
          borderRadius:2,
          columnWidth: "50%",
          barHeight: "100%",
        },
      },
      dataLabels: {
        enabled: false,
      },
      stroke: {
        show: true,
        width: 0,
        colors: ["transparent"],
      },
      xaxis: {
        axisBorder: {
          show: false,
        },
        axisTicks: {
          show: false,
        },
        labels: {
          show: false,
        },
      },
      yaxis: {
        labels: {
          show: false,
        },
      },
      axisBorder: {
        show: false,
      },
      fill: {
        opacity: 1,
      },
      tooltip: {
        theme: "dark",
        style: {
          fontFamily: "inherit",
        },
        x: {
          show: false,
        },
        y: {
          formatter: undefined,
        },
      },
    };
    var chart_column_breadbar = new ApexCharts(
      document.querySelector(".breadbar"),
      breadbar
    );
    chart_column_breadbar.render();


    // -----------------------------------------------------------------------
    // breacrumb chart
    // -----------------------------------------------------------------------
    var breadbar2 = {
      series: [
        {
          name: "",
          data: [5, 8, 7, 12, 6, 7, 15,20],
        },
      ],
      chart: {
        type: "bar",
        width: 70,
        height: 40,
        toolbar: {
          show: false,
        },
        sparkline: {
          enabled: true,
        },
      },
      colors: ["var(--bs-secondary)"],
      grid: {
        show: false,
      },
      plotOptions: {
        bar: {
          horizontal: false,
          borderRadius:2,
          columnWidth: "50%",
          barHeight: "100%",
        },
      },
      dataLabels: {
        enabled: false,
      },
      stroke: {
        show: true,
        width: 0,
        colors: ["transparent"],
      },
      xaxis: {
        axisBorder: {
          show: false,
        },
        axisTicks: {
          show: false,
        },
        labels: {
          show: false,
        },
      },
      yaxis: {
        labels: {
          show: false,
        },
      },
      axisBorder: {
        show: false,
      },
      fill: {
        opacity: 1,
      },
      tooltip: {
        theme: "dark",
        style: {
          fontFamily: "inherit",
        },
        x: {
          show: false,
        },
        y: {
          formatter: undefined,
        },
      },
    };
    var chart_column_breadbar2 = new ApexCharts(
      document.querySelector(".breadbar2"),
      breadbar2
    );
    chart_column_breadbar2.render();
  
  });
  