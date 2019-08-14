package com.chii.server.tt2anjian.Utils;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class SafePasswd {

    public static void main(String[] args) {

        String salt = "admin";
        String password = safe_password("58ab0aa429cbe3e4a7ece65a4dd5cdfc", salt,10);
        System.out.println("salt\n" + salt + "\npassword\n" + password);

    }

    public static String safe_password(String securePassword, String salt,int max) {
        long startTime = System.currentTimeMillis();    //获取开始时间
        for (int i = 0; i < max; i++) {
            securePassword = getSha512(securePassword, salt);
        }
        long endTime = System.currentTimeMillis();    //获取结束时间
        System.out.println(securePassword);
        System.out.println("程序运行时间：" + (endTime - startTime) + "ms");
        return securePassword;
    }

    public static String getSha512(String str, String salt) {
        MessageDigest messagedigest = null;
        try {
            messagedigest = MessageDigest.getInstance("SHA-512");
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        }
        messagedigest.update(salt.getBytes());
        byte[] bytes = messagedigest.digest(str.getBytes());
        StringBuilder sb = new StringBuilder();
        for (byte aByte : bytes) {
            sb.append(Integer.toString((aByte & 0xff) + 0x100, 16).substring(1));
        }
        return sb.toString();
    }
}
