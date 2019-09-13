import { BaseVM } from '@common/BaseModel';

export class Message extends BaseVM {
    text;
    groupId;
    userId;
    date;
    type;
}

export class TempMessage{
    text;
    groupId;
}