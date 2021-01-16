import React from 'react';
import { Theme, withStyles, TableContainer, Table, TableHead, TableRow, TableCell, Paper, TableBody, IconButton, Select, MenuItem, Typography } from "@material-ui/core";
import { IBaseModalProps } from '../../interfaces/modal-interafaces';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';
import { ITreeEntity } from '../../interfaces/ITreeEntity';
import { PermissionsLogic } from '../../logic/permissions-logic';
import { Permission } from '../../../models/Permission';
import { ENUMPermissionType } from '../../../enums/ENUMPermissionType';
import { Delete, Edit, Add, Clear } from '@material-ui/icons';
import { User } from '../../../models/User';
import { ENUMUserType } from '../../../enums/ENUMUserType';
import { UserService } from '../../../login/logic/user-service';


interface IEditPermissionsModalProps extends IBaseModalProps {
    classes: ClassNameMap,
    entity: ITreeEntity | null,
    closeModal: () => void
}
interface IEditPermissionsModalState {
    permissions: Permission[],
    users: User[],
    errorMessage: string,
    selectedUser: User | null,
    selectedPermissionType: ENUMPermissionType,
    showErrorMessage: boolean
}

const styles = (theme: Theme) => ({
    paper: {
        width: 400,
        backgroundColor: theme.palette.background.paper,
        border: '2px solid #000',
        boxShadow: theme.shadows[5],
        padding: theme.spacing(2, 4, 3),
    },
    errorPaper: {
        backgroundColor: '#cb2431',
        padding: theme.spacing(1),
    }
});

class EditPermissionsModal extends React.Component<IEditPermissionsModalProps, IEditPermissionsModalState> {
    state: IEditPermissionsModalState = {
        permissions: [],
        users: [],
        errorMessage: "",
        selectedUser: { id: -1, name: '', userType: ENUMUserType.Normal },
        selectedPermissionType: ENUMPermissionType.View,
        showErrorMessage: false
    }

    getModalStyle = () => {

        return {
            top: `30%`,
            left: `50%`,
            transform: `translate(-50%, -50%)`,
        };
    }

    getPermissions = async () => {
        if (this.props.entity) {
            const response = await PermissionsLogic.getPermissions(this.props.entity);

            if (response.message) {
                this.setState({ errorMessage: response.message, showErrorMessage: true });
            } else if (response.data) {
                this.setState({ permissions: response.data })
                this.setState({ users: this.state.users.filter(this.userExistsInPermissions(response.data)) })
            }
        }
    }

    getUsers = async () => {
        const response = await PermissionsLogic.getUsers();

        if (response.message) {
            this.setState({ errorMessage: response.message, showErrorMessage: true });
        } else if (response.data) {
            this.setState({ users: response.data.filter(this.userExistsInPermissions(this.state.permissions)) })
        }
    }

    userExistsInPermissions = (permissions: Permission[]) => (user: User) => {
        return (!permissions || permissions.findIndex(permission => permission.user.id === user.id) < 0)
            && user.id !== UserService.getCurrentUser()?.id
    }

    permissionTypeText = {
        [ENUMPermissionType.Edit]: 'Edit',
        [ENUMPermissionType.View]: 'View'
    }

    componentDidMount() {
        this.getPermissions();
        this.getUsers();
    }

    handleUserChange = (event: any) => {
        const user = this.state.users.find(user => user.id === event.target.value) || null;
        this.setState({ selectedUser: user })
    }
    handlePermChange = (event: any) => {
        this.setState({ selectedPermissionType: event.target.value })
    }

    handleDelete = (permission: Permission) => async (event: any) => {
        const message = await PermissionsLogic.DeletePermission(permission)

        if (message) {
            this.setState({ errorMessage: message, showErrorMessage: true });
        } else {
            this.getPermissions();
        }
    }

    handleEdit = (permission: Permission) => async (event: any) => {
        const message = await PermissionsLogic.EditPermission(permission)

        if (message) {
            this.setState({ errorMessage: message, showErrorMessage: true });
        } else {
            this.getPermissions();
        }
    }

    handleAdd = async (event: any) => {
        if (this.props.entity && this.state.selectedUser && this.state.selectedUser.id != -1) {
            const message = await PermissionsLogic.AddPermission(this.state.selectedUser, this.props.entity, this.state.selectedPermissionType);

            if (message) {
                this.setState({ errorMessage: message, showErrorMessage: true });
            } else {
                this.getPermissions();
            }
        }
    }

    handleClickCloseError = () => {
        this.setState({ ...this.state, showErrorMessage: false, errorMessage: '' })
    }

    render() {
        const { entity, classes } = this.props;
        const { permissions, selectedUser, selectedPermissionType, users, showErrorMessage, errorMessage } = this.state;

        return (
            <div style={{ ...(this.getModalStyle()), position: 'absolute' }}>
                {showErrorMessage &&
                    <Paper className={classes.errorPaper}>
                        <Typography variant="body2" className={classes.errorText}>{errorMessage}
                            <IconButton
                                onClick={this.handleClickCloseError}
                                aria-label="close error message"
                                edge="end"
                            >
                                <Clear />
                            </IconButton>
                        </Typography>
                    </Paper>
                }
                <TableContainer component={Paper}>
                    <Table className={classes.table} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <TableCell>Username</TableCell>
                                <TableCell >Permission</TableCell>
                                <TableCell >Delete</TableCell>
                                <TableCell >Toggle Edit/View</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {permissions && permissions.map((row) => (
                                <TableRow key={row.id}>
                                    <TableCell component="th" scope="row">
                                        {row.user.name}
                                    </TableCell>
                                    <TableCell >{this.permissionTypeText[row.permissionType]}</TableCell>
                                    <TableCell >
                                        <IconButton color="primary" aria-label="delete" component="span">
                                            <Delete onClick={this.handleDelete(row)} />
                                        </IconButton>
                                    </TableCell>
                                    <TableCell >
                                        <IconButton color="primary" aria-label="edit" component="span">
                                            <Edit onClick={this.handleEdit(row)} />
                                        </IconButton>
                                    </TableCell>
                                </TableRow>
                            ))}
                            {selectedUser && <TableRow>
                                <TableCell>
                                    <Select
                                        labelId="userselect"
                                        id="userselect"
                                        value={selectedUser.id}
                                        onChange={this.handleUserChange}
                                    >
                                        {users && users.map(user => (
                                            <MenuItem value={user.id}>
                                                <em>{user.name}</em>
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </TableCell>
                                <TableCell>
                                    <Select labelId="permselect"
                                        id="permselect"
                                        value={selectedPermissionType}
                                        onChange={this.handlePermChange}>
                                        <MenuItem value={ENUMPermissionType.View}>
                                            {this.permissionTypeText[ENUMPermissionType.View]}
                                        </MenuItem>
                                        <MenuItem value={ENUMPermissionType.Edit}>
                                            {this.permissionTypeText[ENUMPermissionType.Edit]}
                                        </MenuItem>
                                    </Select>
                                </TableCell>
                                <TableCell>
                                    <IconButton color="primary" aria-label="add" component="span">
                                        <Add onClick={this.handleAdd} />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                            }
                        </TableBody>
                    </Table>
                </TableContainer>
            </ div>
        )
    }
}

export default withStyles(styles, { withTheme: true })(EditPermissionsModal)
