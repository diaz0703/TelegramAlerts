using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TelegramTest.Modelos
{
    public class Alerta
    {
        [JsonPropertyName("schemaId")]
        public string SchemaId { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("essentials")]
        public Essentials Essentials { get; set; }

        [JsonPropertyName("alertContext")]
        public AlertContext AlertContext { get; set; }

        [JsonPropertyName("customProperties")]
        public object CustomProperties { get; set; }
    }

    public class Essentials
    {
        [JsonPropertyName("alertId")]
        public string AlertId { get; set; }

        [JsonPropertyName("alertRule")]
        public string AlertRule { get; set; }

        [JsonPropertyName("targetResourceType")]
        public string TargetResourceType { get; set; }

        [JsonPropertyName("alertRuleID")]
        public string AlertRuleID { get; set; }

        [JsonPropertyName("severity")]
        public string Severity { get; set; }

        [JsonPropertyName("signalType")]
        public string SignalType { get; set; }

        [JsonPropertyName("monitorCondition")]
        public string MonitorCondition { get; set; }

        [JsonPropertyName("targetResourceGroup")]
        public string TargetResourceGroup { get; set; }

        [JsonPropertyName("monitoringService")]
        public string MonitoringService { get; set; }

        [JsonPropertyName("alertTargetIDs")]
        public List<string> AlertTargetIDs { get; set; }

        [JsonPropertyName("configurationItems")]
        public List<string> ConfigurationItems { get; set; }

        [JsonPropertyName("originAlertId")]
        public string OriginAlertId { get; set; }

        [JsonPropertyName("firedDateTime")]
        public DateTime FiredDateTime { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("essentialsVersion")]
        public string EssentialsVersion { get; set; }

        [JsonPropertyName("alertContextVersion")]
        public string AlertContextVersion { get; set; }

        [JsonPropertyName("investigationLink")]
        public string InvestigationLink { get; set; }
    }

    public class AlertContext
    {
        [JsonPropertyName("properties")]
        public object Properties { get; set; }

        [JsonPropertyName("conditionType")]
        public string ConditionType { get; set; }

        [JsonPropertyName("condition")]
        public Condition Condition { get; set; }
    }

    public class Condition
    {
        [JsonPropertyName("windowSize")]
        public string WindowSize { get; set; }

        [JsonPropertyName("allOf")]
        public List<AllOf> AllOf { get; set; }

        [JsonPropertyName("staticThresholdFailingPeriods")]
        public StaticThresholdFailingPeriods StaticThresholdFailingPeriods { get; set; }

        [JsonPropertyName("windowStartTime")]
        public DateTime WindowStartTime { get; set; }

        [JsonPropertyName("windowEndTime")]
        public DateTime WindowEndTime { get; set; }
    }

    public class AllOf
    {
        [JsonPropertyName("metricName")]
        public string MetricName { get; set; }

        [JsonPropertyName("metricNamespace")]
        public string MetricNamespace { get; set; }

        [JsonPropertyName("operator")]
        public string Operator { get; set; }

        [JsonPropertyName("threshold")]
        public string Threshold { get; set; }

        [JsonPropertyName("timeAggregation")]
        public string TimeAggregation { get; set; }

        [JsonPropertyName("dimensions")]
        public List<object> Dimensions { get; set; }

        [JsonPropertyName("metricValue")]
        public double MetricValue { get; set; }

        [JsonPropertyName("webTestName")]
        public object WebTestName { get; set; }
    }

    public class StaticThresholdFailingPeriods
    {
        [JsonPropertyName("numberOfEvaluationPeriods")]
        public int NumberOfEvaluationPeriods { get; set; }

        [JsonPropertyName("minFailingPeriodsToAlert")]
        public int MinFailingPeriodsToAlert { get; set; }
    }
}
