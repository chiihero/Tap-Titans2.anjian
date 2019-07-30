package com.chii.server.tt2anjian.mapper;

import com.chii.server.tt2anjian.pojo.Infos;

public interface InfosMapper {
    int deleteByPrimaryKey(Integer iid);

    int insert(Infos record);

    int insertSelective(Infos record);

    Infos selectByPrimaryKey(Integer iid);

    int updateByPrimaryKeySelective(Infos record);

    int updateByPrimaryKey(Infos record);
}