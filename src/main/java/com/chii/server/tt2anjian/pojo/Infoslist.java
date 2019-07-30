package com.chii.server.tt2anjian.pojo;

import java.util.Date;
import java.util.List;

public class Infoslist {
    private Integer mid;

    private String title;

    private Date time;

    private String username;

    private List<Infos> infos;

    public Infoslist(Integer mid, String title, Date time, String username,List<Infos> infos) {
        this.mid = mid;
        this.title = title;
        this.time = time;
        this.username = username;
        this.infos = infos;
    }

    public Infoslist() {
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

    public void setInfos(List<Infos> infos) {
        this.infos = infos;
    }
    public List<Infos> getInfos() {
        return infos;
    }

    public class Infos {
        private Integer iid;

        private Integer mid;

        private String layer;

        private String usetime;

        public Infos(Integer iid, Integer mid, String layer, String usetime) {
            this.iid = iid;
            this.mid = mid;
            this.layer = layer;
            this.usetime = usetime;
        }

        public Infos() {
            super();
        }

        public Integer getIid() {
            return iid;
        }

        public void setIid(Integer iid) {
            this.iid = iid;
        }

        public Integer getMid() {
            return mid;
        }

        public void setMid(Integer mid) {
            this.mid = mid;
        }

        public String getLayer() {
            return layer;
        }

        public void setLayer(String layer) {
            this.layer = layer == null ? null : layer.trim();
        }

        public String getUsetime() {
            return usetime;
        }

        public void setUsetime(String usetime) {
            this.usetime = usetime == null ? null : usetime.trim();
        }
    }
}