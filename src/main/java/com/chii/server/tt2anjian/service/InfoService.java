package com.chii.server.tt2anjian.service;

import com.chii.server.tt2anjian.pojo.Info;

import java.util.List;

public interface InfoService {

    List<Info> getInfoInfoByUsername(String username);

    void insertInfo(Info info);

    void deleteInfo(Info info);

}
