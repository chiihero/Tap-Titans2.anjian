package com.chii.server.tt2anjian.pojo;

public class User {
    private Integer uid;

    private String username;

    private String passwd;

    public User(Integer uid, String username, String passwd) {
        this.uid = uid;
        this.username = username;
        this.passwd = passwd;
    }

    public User() {
        super();
    }

    public Integer getUid() {
        return uid;
    }

    public void setUid(Integer uid) {
        this.uid = uid;
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
}