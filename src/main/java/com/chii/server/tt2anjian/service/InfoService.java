package com.chii.server.tt2anjian.service;

import com.chii.server.tt2anjian.pojo.Info;
import com.chii.server.tt2anjian.pojo.PageBean;
import com.github.pagehelper.PageInfo;

import java.util.List;

public interface InfoService {

    PageInfo<Info> getInfoInfoByUsername(PageBean pageBean);

//    List<Info> getInfoLastByUsername(String username);


    int insertInfo(Info info);

    void deleteInfo(int mid);

}
