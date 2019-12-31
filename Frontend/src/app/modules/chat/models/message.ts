import { BaseVM, AddVM, UpdateVM } from '@app/shared/models/BaseModel';

export class MessageVM extends BaseVM {
    public text: string;
    public groupId: string;
    public userId: string;
    public date: Date;
    public type: string;
}

export class MessageAddVM extends AddVM {
    public text: string;
    public groupId: string;
    public userId: string;
    public date: Date;
}

export class MessageUpdateVM extends UpdateVM {
    public text: string;
}

export class TempMessage{
    public text: string;
    public groupId: string;
}

export class MessageSearchVM extends BaseVM {
    public text: string;
    public groupId: string;
    public userId: string;
    public createDT: Date;
}
