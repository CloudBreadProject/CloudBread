package com.example.dwtechdaysdemo;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

import android.util.Base64;

public class SHA_encrypt {
    private static String userPassword;

    /**
     * 패스워드 암호화
     * @param userPassword
     *            사용자 패스워드
     * @return 암호화 된 사용자 패스워드
     *         암호화 방식 : SHA-512
     */
    public static String encryption(String userPassword) {
        MessageDigest md;
        
        String tempPassword = "";
        
        try {
            md = MessageDigest.getInstance("SHA-512");

            md.update(userPassword.getBytes("UTF-8"));
            byte[] mb = md.digest();
            for (int i = 0; i < mb.length; i++) {
                byte temp = mb[i];
                String s = Integer.toHexString(new Byte(temp));
                while (s.length() < 2) {
                    s = "0" + s;
                }
                s = s.substring(s.length() - 2);
                tempPassword += s;
            }
         
        } catch (NoSuchAlgorithmException e) {
        	return null;
        }
	     catch (UnsupportedEncodingException e) {
	    	return null;
	    }
        
        return Base64.encodeToString(tempPassword.getBytes(), Base64.DEFAULT);
    }
    
}