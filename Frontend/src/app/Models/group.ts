export class Group{
    id;
    name;
    desctiption;
    isMain;
    isPrivate;

    userList;

    constructor(_id, _name, _description, _isMain, _isPrivate, _userList = null){
        this.id = _id;
        this.name = _name;
        this.desctiption = _description;
        this.isMain = _isMain;
        this.isPrivate = _isPrivate;

        if(_userList != null){
            this.userList = _userList;
        }
    }
}