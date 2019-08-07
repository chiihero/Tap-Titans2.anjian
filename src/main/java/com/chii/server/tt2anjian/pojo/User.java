package com.chii.server.tt2anjian.pojo;

public class User {
    private String username;

    private String passwd;

    private String mail;

    public User(String username, String passwd, String mail) {
        this.username = username;
        this.passwd = passwd;
        this.mail = mail;
    }

    public User() {
        super();
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username == null ? null : username.trim();
    }

    public String getPasswd() {
        return passwd;
    }

    public void setPasswd(String passwd) {
        this.passwd = passwd == null ? null : passwd.trim();
    }

    public String getMail() {
        return mail;
    }

    public void setMail(String mail) {
        this.mail = mail == null ? null : mail.trim();
    }
}