package com.example.dwtechdaysdemo;

import javax.crypto.BadPaddingException;
import javax.crypto.Cipher;
import javax.crypto.IllegalBlockSizeException;
import javax.crypto.NoSuchPaddingException;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;

import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;
import java.security.InvalidAlgorithmParameterException;
import java.security.spec.AlgorithmParameterSpec;
import android.util.Base64;


public class AES256Cipher {
	
	public static byte[] ivBytes = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
	public static byte[] keyBytes = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };


	public static String AES_Encode(String str, String key)	throws java.io.UnsupportedEncodingException, NoSuchAlgorithmException, NoSuchPaddingException, InvalidKeyException, InvalidAlgorithmParameterException,	IllegalBlockSizeException, BadPaddingException {
		
		String ivstr = "1234567890123456";
		//IV = secretKey.substring(0,16); 
		byte[] tempivBytes = ivstr.getBytes();
		byte[] textBytes = str.getBytes("UTF-8");
		
    	for(int i = 0; i < tempivBytes.length ; i++) 
    	{
    		if(i>=16)
    			break;
    		ivBytes[i] = tempivBytes[i];
    	}
        	
      	
	
        byte[] TempkeyBytes = key.getBytes("UTF-8");
		for(int i = 0; i < TempkeyBytes.length ; i++) 
    	{
			if(i>=16)
    			break;
    		keyBytes[i] = TempkeyBytes[i];
    	}
		
		AlgorithmParameterSpec ivSpec = new IvParameterSpec(ivBytes);
		     SecretKeySpec newKey = new SecretKeySpec(keyBytes, "AES");
		     Cipher cipher = null;
		cipher = Cipher.getInstance("AES/CBC/PKCS7Padding");
//	    cipher = Cipher.getInstance("AES/ECB/PKCS5Padding");
		cipher.init(Cipher.ENCRYPT_MODE, newKey, ivSpec);
//		return cipher.doFinal(textBytes).toString();
		byte[] TemptextBytes = cipher.doFinal(textBytes);
		return Base64.encodeToString(TemptextBytes, Base64.DEFAULT);
	}

	public static String AES_Decode(String str, String key)	throws java.io.UnsupportedEncodingException, NoSuchAlgorithmException, NoSuchPaddingException, InvalidKeyException, InvalidAlgorithmParameterException, IllegalBlockSizeException, BadPaddingException {
		
		byte[] textBytes =Base64.decode(str,0);
//		byte[] textBytes = str.getBytes("UTF-8");
		String ivstr = "1234567890123456";
	
		byte[] tempivBytes = ivstr.getBytes();

	
    	for(int i = 0; i < tempivBytes.length ; i++) 
    	{
    		if(i>=16)
    			break;
    		ivBytes[i] = tempivBytes[i];
    	}
  	
	
        byte[] TempkeyBytes = key.getBytes("UTF-8");
    	
    	for(int i = 0; i < TempkeyBytes.length ; i++) 
    	{
    		if(i>=16)
    			break;
    		keyBytes[i] = TempkeyBytes[i];
    	}

		AlgorithmParameterSpec ivSpec = new IvParameterSpec(ivBytes);
		SecretKeySpec newKey = new SecretKeySpec(keyBytes, "AES");
		Cipher cipher = Cipher.getInstance("AES/CBC/PKCS7Padding");
//		Cipher cipher = Cipher.getInstance("AES/ECB/PKCS5Padding");
		cipher.init(Cipher.DECRYPT_MODE, newKey, ivSpec);
		return new String(cipher.doFinal(textBytes), "UTF-8");
	}
}