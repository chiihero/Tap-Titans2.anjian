package com.chii.server.tt2anjian.mapper;

import com.chii.server.tt2anjian.pojo.Info;
import com.chii.server.tt2anjian.pojo.Infos;
import org.apache.ibatis.annotations.Mapper;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface InfosMapper {
    int deleteByPrimaryKey(Integer iid);

    int insert(Infos record);

    int insertSelective(Infos record);

    Infos selectByPrimaryKey(Integer iid);

    List<Infos> selectByMid(Integer mid);

    int updateByPrimaryKeySelective(Infos record);

    int updateByPrimaryKey(Infos record);
}