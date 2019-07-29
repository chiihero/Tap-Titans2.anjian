package com.chii.server.tt2anjian.pojo;

import java.util.Date;

public class Info {
    private Integer mid;

    private String title;

    private Date time;

    private String username;

    public Info(Integer mid, String title, Date time, String username) {
        this.mid = mid;
        this.title = title;
        this.time = time;
        this.username = username;
    }

    public Info() {
        super();
    }

    public Integer getMid() {
        return mid;
    }

    public void setMid(Integer mid) {
        this.mid = mid;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title == null ? null : title.trim();
    }

    public Date getTime() {
        return time;
    }

    public void setTime(Date time) {
        this.time = time;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username == null ? null : username.trim();
    }
}