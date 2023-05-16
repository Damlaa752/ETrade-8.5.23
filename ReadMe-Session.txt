
-- Session 
-Siteye giren kullanýcýya göre herhangi bir type'de veri tutmaya yarar.
-Session verileri serverda tutar.

1-Session için gerekli type'de bir class oluþturulur
Ör : 1. CartItem class
	 2. Session için Get ve Set metotlarý barýndýran bir static class ý. 
			* Sessiondan veri alma(Get) ve veri çekme string tipte olmalýdýr.
			* String bir tipi classa çevirmek için string ifadenin json formatýnda olmasý daha uygundur.
	 3. Program.cs ' de AddSession ve UseSession tanýmlanýr.
	 4. Controller ' da session iþlemleri yapýlýr.