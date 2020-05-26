namespace Models
{
 public class CountOutboxResult : CountInboxResult
 {
	public long SumPart { get; set; }
	public long Cost { get; set; }
 }
}