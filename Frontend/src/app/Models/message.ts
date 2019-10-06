import { BaseVM, AddVM, UpdateVM } from '@app/common/baseModel';

export class MessageVM extends BaseVM {
    public text: String;
    public groupId: String;
    public userId: String;
    public date: Date;
    public type: String;
}

export class MessageAddVM extends AddVM {
    public text: String;
    public groupId: String;
    public userId: String;
    public date: Date;
}

export class MessageUpdateVM extends UpdateVM {
    public text: String;
}

export class TempMessage{
    public text: String;
    public groupId: String;
}

export class MessageSearchVM extends BaseVM {
    public text: String;
    public groupId: String;
    public userId: String;
    public createDT: Date;
}
