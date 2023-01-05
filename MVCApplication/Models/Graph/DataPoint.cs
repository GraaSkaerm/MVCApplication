using System.Runtime.Serialization;

namespace MVCApplication.Models
{
	[DataContract]
	public class DataPoint
	{
		[DataMember(Name = "y")]
		public int Value { get; }

		[DataMember(Name = "label")]
		public string Label { get; }

		public DataPoint(string label, int value)
		{
			this.Label = label;
			this.Value = value;
		}

	}
}
