
-- Session 
-Siteye giren kullan�c�ya g�re herhangi bir type'de veri tutmaya yarar.
-Session verileri serverda tutar.

1-Session i�in gerekli type'de bir class olu�turulur
�r : 1. CartItem class
	 2. Session i�in Get ve Set metotlar� bar�nd�ran bir static class �. 
			* Sessiondan veri alma(Get) ve veri �ekme string tipte olmal�d�r.
			* String bir tipi classa �evirmek i�in string ifadenin json format�nda olmas� daha uygundur.
	 3. Program.cs ' de AddSession ve UseSession tan�mlan�r.
	 4. Controller ' da session i�lemleri yap�l�r.