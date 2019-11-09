package com.chii.server.tt2anjian.controller;


import com.chii.server.tt2anjian.Tt2anjianApplication;
import com.chii.server.tt2anjian.Utils.SafePasswd;
import com.chii.server.tt2anjian.pojo.Info;
import com.chii.server.tt2anjian.pojo.Infos;
import com.chii.server.tt2anjian.pojo.User;
import com.chii.server.tt2anjian.pojo.postlist;

import com.chii.server.tt2anjian.service.InfoService;
import com.chii.server.tt2anjian.service.InfosService;
import com.chii.server.tt2anjian.service.UserService;
import com.github.pagehelper.PageHelper;
import com.github.pagehelper.PageInfo;
import com.google.gson.Gson;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.Date;
import java.util.List;


@RestController
@RequestMapping("/info")
public class InfoController {
    @Autowired
    private InfoService infoService;
    @Autowired
    private InfosService infosService;
    @Autowired
    private UserService userService;

    private static final Logger logger = LoggerFactory.getLogger(Tt2anjianApplication.class);

    @GetMapping("/getinfolist")
    public List<Info> getInfoByUsername(@ModelAttribute("username") String username) {
        List<Info> infoList = infoService.getInfoInfoByUsername(username);
        logger.info(new Gson().toJson(infoList));
        return infoList;
    }
    @GetMapping("/getinfopage")
    public PageInfo<Info> getInfoByUsernamepage(@ModelAttribute("username") String username,@ModelAttribute("pageNum") String pageNum,@ModelAttribute("pageSize") String pageSize) {

        PageHelper.startPage(Integer.valueOf(pageNum),Integer.valueOf(pageSize));
        List<Info> infoList = infoService.getInfoInfoByUsername(username);
        logger.info(new Gson().toJson(infoList));
        return new PageInfo<>(infoList);
    }
    @GetMapping("/getinfos")
    public List<Infos> getInfosByMid(@ModelAttribute("mid") int mid) {
        List<Infos> infos = infosService.getInfosByMid(mid);
        logger.info(new Gson().toJson(infos));
        return infos;
    }

    @PostMapping("/insertinfo")
    public void insertinfo(@RequestBody String json) {
//        logger.info(json);
        Gson gson = new Gson();
        Info info = new Info();
        Infos infos = new Infos();

        postlist postlist = gson.fromJson(json, postlist.class);
        logger.info(new Gson().toJson(postlist));
        //验证身份
        User user = userService.getUserInfoByUsername(postlist.getUsername());
        String passwd = SafePasswd.safe_password(postlist.getPasswd(), postlist.getUsername(), 10);
        if (user != null && user.getPasswd().equals(passwd)) {
            logger.info("login success");
        } else {
            return;
        }
        info.setTitle(postlist.getTitle());
        info.setNotes(postlist.getNotes());
        info.setUsername(postlist.getUsername());
        info.setLayerSet(postlist.getLayerSet());
        info.setUpdateAll(postlist.getUpdateAll());
        info.setUpdateMini(postlist.getUpdateMini());
        infoService.insertInfo(info);
        for (postlist.Infos infosone : postlist.getInfos()) {
            if (info.getMid() == null) break;
            infos.setMid(info.getMid());
            infos.setLayer(infosone.getLayer());
            infos.setUsetime(infosone.getUsetime());
            infosService.insertInfos(infos);
        }
    }
    @PostMapping("/deleteAll")
    public String deleteAll(@ModelAttribute("username") String username, @ModelAttribute("passwd") String passwd) {
        User user = userService.getUserInfoByUsername(username);
        if (user != null) {
            passwd = SafePasswd.safe_password(passwd, username, 10);
            if (user.getPasswd().equals(passwd)) {
                logger.info("deleteAll login success");
                infoService.deleteAllInfoByUser(username);
                return "true";
            }
        }
        return "false";

    }
    @PostMapping("/delete")
    public String delete(@ModelAttribute("username") int username, @ModelAttribute("passwd") String passwd,@ModelAttribute("mid") int mid) {
        infoService.deleteInfo(mid);
        return "true";
    }
}
