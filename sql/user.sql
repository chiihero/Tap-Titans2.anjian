/*
 Navicat Premium Data Transfer

 Source Server         : tt2
 Source Server Type    : MySQL
 Source Server Version : 50562
 Source Host           : 139.199.11.57:3306
 Source Schema         : tt2anjian

 Target Server Type    : MySQL
 Target Server Version : 50562
 File Encoding         : 65001

 Date: 05/08/2019 23:26:29
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user`  (
  `username` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `passwd` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `mail` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`username`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('chii', 'chenliji1', NULL);
INSERT INTO `user` VALUES ('chii1', 'chii1', NULL);

SET FOREIGN_KEY_CHECKS = 1;
