using System;
using Kavenegar.Core.Utils;

namespace Models
{
 public class AccountInfoResult
 {
	public long RemainCredit { get; set; }
	public long Expiredate { get; set; }
	public DateTime GregorianExpiredate
	{
	 get { return DateHelper.UnixTimestampToDateTime(Expiredate); }
	}
	public string Type { get; set; }
 }
}