package com.chii.tt2info.pojo;

import java.sql.Timestamp;
import java.util.Date;
import java.util.List;

public class postlist {
    private Integer mid;

    private String title;

    private String notes;

    private Integer layerSet;

    private Integer updateAll;

    private Integer updateMini;

    private Timestamp time;

    private String username;

    private String passwd;

    private List<Infos> infos;

    public postlist(Integer mid, String title, String notes, Integer layerSet, Integer updateAll, Integer updateMini, Timestamp time, String username,String passwd, List<Infos> infos) {
        this.mid = mid;
        this.title = title;
        this.notes = notes;
        this.layerSet = layerSet;
        this.updateAll = updateAll;
        this.updateMini = updateMini;
        this.time = time;
        this.username = username;
        this.passwd =passwd;
        this.infos = infos;
    }

    public postlist() {
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

    public String getNotes() {
        return notes;
    }

    public void setNotes(String notes) {
        this.notes = notes == null ? null : notes.trim();
    }

    public Integer getLayerSet() {
        return layerSet;
    }

    public void setLayerSet(Integer layerSet) {
        this.layerSet = layerSet;
    }

    public Integer getUpdateAll() {
        return updateAll;
    }

    public void setUpdateAll(Integer updateAll) {
        this.updateAll = updateAll;
    }

    public Integer getUpdateMini() {
        return updateMini;
    }

    public void setUpdateMini(Integer updateMini) {
        this.updateMini = updateMini;
    }

    public Timestamp getTime() {
        return time;
    }

    public void setTime(Timestamp time) {
        this.time = time;
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