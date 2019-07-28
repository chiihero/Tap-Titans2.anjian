package com.chii.server.tt2anjian.pojo;

import java.util.Date;

public class Info {
    private Integer mid;

    private String title;

    private Date time;

    private Integer uid;

    public Info(Integer mid, String title, Date time, Integer uid) {
        this.mid = mid;
        this.title = title;
        this.time = time;
        this.uid = uid;
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

    public Integer getUid() {
        return uid;
    }

    public void setUid(Integer uid) {
        this.uid = uid;
    }
}