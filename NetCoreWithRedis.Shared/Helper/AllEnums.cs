namespace NetCoreWithRedis.Shared.Entities
{
    public enum ActivationStatusType
    {
        Active = 1,
        Passive = 2,
        Suspend = 3,
        PendingApproval = 4
    }
    public enum ConfirmStatusType
    {
        WaitConfirm = 0,
        HaveConfirm = 1,
        RejectConfirm = 2,
        ResetPassword = 3
    }
    public enum ResponseStatusType
    {
        OK,
        ERROR
    }
    public enum TextTypeEnum
    {
        Menu = 1,
        PlainText = 2,
        ErrorMessage = 3
    }
    public enum UsersTypeEnum
    {
        Root = 1,
        Admin = 2,
        PanelUser = 3
    }
}
