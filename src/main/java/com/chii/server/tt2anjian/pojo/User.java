package com.chii.server.tt2anjian.pojo;

import org.springframework.stereotype.Component;

public class User {
    private String username;

    private String passwd;

    public User(String username, String passwd) {
        this.username = username;
        this.passwd = passwd;
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
}