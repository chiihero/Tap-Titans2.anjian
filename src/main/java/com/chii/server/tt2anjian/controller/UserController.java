package com.chii.server.tt2anjian.controller;


import com.chii.server.tt2anjian.Tt2anjianApplication;
import com.chii.server.tt2anjian.pojo.User;
import com.chii.server.tt2anjian.service.UserService;
import com.google.gson.Gson;
import org.apache.commons.logging.Log;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.Date;


@RestController
//@RequestMapping("/test")
public class UserController {
    @Autowired
    private UserService userService;

    private static final Logger logger = LoggerFactory.getLogger(Tt2anjianApplication.class);

    @GetMapping("/hello")
    public String hello() {
        logger.info("hello chii"+new Date());
        return "hello chii";
    }

    @GetMapping("/user")
    public User userinfo(@ModelAttribute("username") String username) {
        System.out.println(username);
        User user = userService.getUserInfoByUsername(username);
        if (user != null) {
            logger.info(new Gson().toJson(user));
        }
        return user;
    }

    @PostMapping("/login")
    public User login(@ModelAttribute("username") String username, @ModelAttribute("passwd") String passwd) {
        User user = userService.getUserInfoByUsername(username);
        if (user != null && user.getPasswd().equals(passwd)) {
            logger.info("login success");
            return user;
        } else {
            return null;
        }
    }

    @PostMapping("/signin")
    public String signin(@ModelAttribute("username") String username, @ModelAttribute("passwd") String passwd) {
        User user = userService.getUserInfoByUsername(username);
        if (user != null) {
            return "Your username has been registered";
        } else {
            User newuser = new User();
            newuser.setUsername(username);
            newuser.setPasswd(passwd);
            userService.insertUserInfo(newuser);
            return "Your username registered successfully";
        }
    }
}
