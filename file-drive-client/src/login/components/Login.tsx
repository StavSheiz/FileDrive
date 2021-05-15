import React from 'react';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import { withStyles, Theme } from '@material-ui/core/styles';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';
import { Typography, InputAdornment, IconButton, OutlinedInput, InputLabel, FormControl, Button, fade } from '@material-ui/core';
import { Visibility, VisibilityOff, Clear } from '@material-ui/icons/';
import { LoginLogic } from '../logic/login-logic';
import { RouteComponentProps, withRouter, Link } from 'react-router-dom';


type ILoginProps = RouteComponentProps & {
    classes: ClassNameMap,
    history: any
}
interface ILoginState {
    password: string,
    name: string,
    showPassword: boolean,
    errorMessage: string,
    showErrorMessage: boolean
}

const styles = (theme: Theme) => ({
    root: {
        flexGrow: 1,
        overflow: 'hidden',
        padding: theme.spacing(0, 3),
    },
    container: {
        maxWidth: 400,
        margin: `${theme.spacing(1)}px auto`,
    },
    loginPaper: {
        padding: theme.spacing(4),
    },
    newAccountPaper: {
        padding: theme.spacing(2),
    },
    signInButton: {
        backgroundColor: '#2c974b',
        color: '#ffffff',
        width: '100%'
    },
    logo: {
        height: 48,
        width: 48,
        color: '#586069'
    },
    errorPaper: {
        backgroundColor: fade('#cb2431', 0.5),
        padding: theme.spacing(1),
    }
});

class Login extends React.Component<ILoginProps, ILoginState> {
    state: ILoginState = {
        password: '',
        name: '',
        showPassword: false,
        errorMessage: '',
        showErrorMessage: false
    }

    handleClickShowPassword = () => {
        this.setState({ ...this.state, showPassword: !this.state.showPassword });
    };

    handleMouseDownPassword = (event: any) => {
        event.preventDefault();
    };

    handleChange = (prop: string) => (event: any) => {
        this.setState({ ...this.state, [prop]: event.target.value });
    };

    handleSignIn = async () => {
        const { name, password } = this.state;
        const message = await LoginLogic.signIn(name, password);
        if (message) {
            this.setState({ ...this.state, showErrorMessage: true, errorMessage: message, password: '' });
        } else {
            this.props.history.push('/tree');
        }
    }

    handleClickCloseError = () => {
        this.setState({ ...this.state, showErrorMessage: false, errorMessage: '' })
    }

    handleCreateAccount = () => {
        this.props.history.push('/signup');
    }

    render() {
        const { classes } = this.props;
        const { password, showPassword, name, showErrorMessage, errorMessage } = this.state;

        return (
            <div>
                <div className={classes.root}>
                    <Grid container direction="column" spacing={3} className={classes.container}>
                        <Grid item>
                            <img alt={'loading logo...'} src={"logo_transparent.png"} className={classes.logo} />
                        </Grid>
                        <Grid item>
                            <Typography variant="h6">Sign in to FileDrive</Typography>

                        </Grid>
                        {showErrorMessage &&
                            <Grid item>
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
                            </Grid>
                        }
                        <Grid item>
                            <Paper className={classes.loginPaper}>
                                <Grid container direction="column" spacing={3}>
                                    <Grid item>
                                        <Grid container wrap="nowrap" direction="column" spacing={2}>
                                            <Grid item>
                                                <FormControl fullWidth variant="outlined">

                                                    <InputLabel htmlFor="username-input">Username</InputLabel>
                                                    <OutlinedInput
                                                        fullWidth
                                                        onChange={this.handleChange('name')}
                                                        label="Username"
                                                        id="username-input"
                                                        value={name}
                                                    />
                                                </FormControl>
                                            </Grid>
                                        </Grid>
                                    </Grid>
                                    <Grid item>
                                        <Grid container wrap="nowrap" direction="column" spacing={2}>
                                            <Grid item>
                                                <FormControl fullWidth variant="outlined">
                                                    <InputLabel htmlFor="password-input">Password</InputLabel>
                                                    <OutlinedInput
                                                        fullWidth
                                                        onChange={this.handleChange('password')}
                                                        label="Password"
                                                        id="password-input"
                                                        type={showPassword ? 'text' : 'password'}
                                                        value={password}
                                                        endAdornment={
                                                            <InputAdornment position="end">
                                                                <IconButton
                                                                    onClick={this.handleClickShowPassword}
                                                                    onMouseDown={this.handleMouseDownPassword}
                                                                    aria-label="toggle password visibility"
                                                                    edge="end"
                                                                >
                                                                    {showPassword ? <Visibility /> : <VisibilityOff />}
                                                                </IconButton>
                                                            </InputAdornment>
                                                        }
                                                    />
                                                </FormControl>
                                            </Grid>
                                        </Grid>
                                    </Grid>
                                    <Grid item>
                                        <Button
                                            className={classes.signInButton}
                                            variant="contained"
                                            disableRipple
                                            onClick={this.handleSignIn}
                                        >Sign in</Button>
                                    </Grid>
                                </Grid>
                            </Paper>
                        </Grid>
                        <Grid item>
                            <Paper className={classes.newAccountPaper}>
                                <Typography variant="body1">
                                    New to FileDrive? <Link to={"signup"} onClick={this.handleCreateAccount} color={"textPrimary"} > Create an account. </Link> {/* TODO: Redirect to create account */}
                                </Typography>
                            </Paper>
                        </Grid>
                    </Grid>
                </div>
            </div>
        );
    }
}

export default withRouter(withStyles(styles, { withTheme: true })(Login))
