import os
from typing import Dict
import json
import requests
import sys


# 将token从文件中读取出来后续使用
def tokenRead() -> str:
    f = open("token.txt", "r")
    user_token = f.read()
    f.close()
    return user_token


# 用户登录
def logIn(student_id: str, password: str) -> json:
    url = "http://172.17.173.97:8080/api/user/login"
    data = {"student_id": student_id, "password": password}
    results = requests.post(url=url, data=data)
    if results.json()["status"] != 200:
        return json.dumps(results.json())
    # 由于命令行参数长度限制需要将token记录到文件中以便后续使用
    f = open("token.txt", "w")
    f.write(results.json()["data"]["token"])
    f.close()
    return json.dumps(results.json())


# 创建对局
def createContest(privacy: bool, user_token: str):
    url = "http://172.17.173.97:9000/api/game"
    data = {"private": privacy}
    headers = {"Authorization": "Bearer " + user_token}
    results = requests.post(url=url, headers=headers, data=data)
    return json.dumps(results.json())


# 加入对局
def joinContest(uuid: str, user_token: str):
    url = "http://172.17.173.97:9000/api/game/" + uuid
    headers = {"Authorization": "Bearer " + user_token}
    results = requests.post(url=url, headers=headers)
    return json.dumps(results.json())


# 执行玩家操作
def executeOperation(uuid: str, user_token: str, how: int, card_type: str):
    url = "http://172.17.173.97:9000/api/game/" + uuid
    headers = {"Authorization": "Bearer " + user_token}
    data: Dict
    if how == 0:
        data = {"type": 0}
    else:
        data = {"type": 1, "card": card_type}
    results = requests.put(url=url, headers=headers, data=data)
    return json.dumps(results.json())


# 获取上步操作
def getPreviousOperation(uuid: str, user_token: str):
    url = "http://172.17.173.97:9000/api/game/" + uuid + "/last"
    headers = {"Authorization": "Bearer " + user_token}
    results = requests.get(url=url, headers=headers)
    return json.dumps(results.json())


# 获取对局信息
def fetchContestInfo(uuid: str, user_token: str):
    url = "http://172.17.173.97:9000/api/game/" + uuid
    headers = {"Authorization": "Bearer " + user_token}
    results = requests.get(url=url, headers=headers)
    return json.dumps(results.json())


# 获取公开对局且目前未完成对局
def fetchPublicContest(user_token: str):
    url = "http://172.17.173.97:9000/api/game/index"
    headers = {"Authorization": "Bearer " + user_token}
    params = {"page_size": "10000", "page_num": "1"}
    results = requests.get(url=url, headers=headers, params=params)
    return json.dumps(results.json())


if __name__ == "__main__":
    # python webHelper.py 参数
    # 对sys.argv[2]进行特殊判断，视为调动某个方法的Key，采用键值映射
    # player_token代表玩家的token
    # 如果不是登录操作即logIn就执行后续的操作例如读取文件等
    if sys.argv[1] == "logIn":
        # Key student_id password
        print(logIn(sys.argv[2], sys.argv[3]))
    else:
        player_token = tokenRead()
        if sys.argv[1] == "createContest":
            # Key privacy(str) user_token
            if sys.argv[2] == "True":
                print(createContest(True, player_token))
            else:
                print(createContest(False, player_token))
        elif sys.argv[1] == "joinContest":
            # Key uuid user_token
            print(joinContest(sys.argv[2], player_token))
        elif sys.argv[1] == "executeOperation":
            # Key uuid user_token how card_type
            if sys.argv[3] == "1":
                print(executeOperation(sys.argv[2], player_token, int(sys.argv[3]), sys.argv[4]))
            else:
                print(executeOperation(sys.argv[2], player_token, int(sys.argv[3]), ""))
        elif sys.argv[1] == "getPreviousOperation":
            # Key uuid user_token
            print(getPreviousOperation(sys.argv[2], player_token))
        elif sys.argv[1] == "fetchContestInfo":
            # Key uuid user_token
            print(fetchContestInfo(sys.argv[2], player_token))
        else:
            # Key user_token
            print(fetchPublicContest(player_token))
        # os.system("pause")
