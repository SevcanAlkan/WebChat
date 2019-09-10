export class Message{
    text;
    groupId;
    userId;

    constructor(text, groupId, userId){
        this.text = text;
        this.groupId = groupId;
        this.userId = userId;
    }
}