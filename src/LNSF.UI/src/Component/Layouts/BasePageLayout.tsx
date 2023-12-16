import { Avatar, Box, Button, Divider, Paper, Popover, Typography, useMediaQuery, useTheme } from "@mui/material"
import MenuIcon from '@mui/icons-material/Menu';
import MenuOpenIcon from '@mui/icons-material/MenuOpen';
import iconelogoProvisoria from '../../assets/icone_logo.svg';
import { AuthContext, iUser, useDrawerContext } from "../../Contexts";
import LogoutIcon from '@mui/icons-material/Logout';
import { useContext, useEffect, useState } from "react";
import { Footer } from "./footer/Footer";


interface IBasePageLayoutProps {
    children: React.ReactNode;

}

export const BasePageLayout: React.FC<IBasePageLayoutProps> = ({ children }) => {
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const mdDown = useMediaQuery(theme.breakpoints.down('md'));
    const { isDrawerOpen, toggleDrawerOpen, } = useDrawerContext();
    // const { toggleTheme } = useAppThemeContext();
    const { getUser,  logout } = useContext(AuthContext)
    const [user, setUser] = useState<iUser>({} as iUser);

    useEffect(() => {
        getUser().then((userData) => {
            setUser(userData);
        });
    }, [getUser]);

   

    const [popoverAnchorEl, setPopoverAnchorEl] = useState<null | HTMLElement>(null);

    const handlePopoverOpen = (event: React.MouseEvent<HTMLElement>) => {
        setPopoverAnchorEl(event.currentTarget);
    };

    const handlePopoverClose = () => {
        setPopoverAnchorEl(null);
    };


    const handleLogout = () => {
        logout();
    }

    const openPopover = Boolean(popoverAnchorEl);
    return (
        <>

            {/* Barra superior */}
            <Box
            >
                <Box
                    width='100%'
                    height={theme.spacing(mdDown ? 9 : 10)}
                    bgcolor={theme.palette.background.paper}
                    display='flex'
                    justifyContent={smDown ? 'space-between' : 'end'}
                    alignItems='center'
                    position="fixed"
                    zIndex={1}
                >
                    {smDown && (
                        <Button
                            color='primary'
                            onClick={toggleDrawerOpen}
                        >
                            {isDrawerOpen ? <MenuOpenIcon /> : <MenuIcon />}
                        </Button>
                    )}

                    <Box
                        width={theme.spacing(28)}
                        height={theme.spacing(9)}
                        display='flex'
                        flexDirection='row'
                        gap={3}
                        alignItems='center'
                        marginRight={theme.spacing(2)}
                    >
                        {/* <Box >
                            <Button onClick={toggleTheme}  >
                                <Brightness6RoundedIcon color='primary' />
                            </Button>
                        </Box> */}

                        <Box>
                            {/* {!smDown && (<Typography */}
                            <Typography
                            //variant={mdDown ? 'body2' : 'subtitle1'}
                            >
                                {user.role.toString()}
                                <Typography fontSize={12}>
                                    {user.userName}
                                </Typography>

                            </Typography>
                        </Box>
                        <Box
                            onClick={handlePopoverOpen}
                        >
                            <Avatar alt="Remy Sharp" src={iconelogoProvisoria} sx={{ width: 30, height: 30 }} />
                        </Box>

                        <Popover
                            open={openPopover}
                            anchorEl={popoverAnchorEl}
                            onClose={handlePopoverClose}
                            anchorOrigin={{
                                vertical: 'bottom',
                                horizontal: 'left',
                            }}
                        >
                            <Box padding={2}
                                display= 'flex'
                                flexDirection='column'
                                alignItems='center'
                                justifyContent= 'center'
                            >
                                <Box
                                    marginBottom={2}>
                                    <Typography
                                        variant={mdDown ? 'body2' : 'subtitle1'}
                                        textAlign='center'
                                    >
                                        {user.role.toString()}
                                        <Typography fontSize={12}>
                                            lnsf@gmail.com
                                        </Typography>
                                    </Typography>
                                </Box>

                                <Divider />

                                <Button onClick={handleLogout}
                                
                                >
                                    <LogoutIcon color='primary' />
                                    Sair
                                </Button>
                            </Box>
                        </Popover>

                    </Box>
                </Box>
            </Box >




            {/* Conteúdo central da página escolhido por uma rota específica */}
            <Box
                height='100%'
                marginLeft={smDown ? theme.spacing(0) : theme.spacing(28)}
                display='flex'
                bgcolor={theme.palette.background.default}
                flexDirection='column'
                gap={theme.spacing(3)}

            >
                <Box
                    display='flex'
                    marginX={smDown ? theme.spacing(2) : mdDown ? theme.spacing(5) : theme.spacing(8)}
                    marginTop={mdDown ? theme.spacing(11) : theme.spacing(12)}
                    padding={theme.spacing(2)}
                    gap={theme.spacing(1)}
                    maxHeight={theme.spacing(70)}
                    overflow='auto'
                    bgcolor={theme.palette.background.paper}
                    component={Paper}
                >
                    {children}
                </Box >

                <Box
                    overflow='hidden'
                    display='flex'
                    flexDirection='column'
                    gap={theme.spacing(3)}
                >
                    <Footer />
                </Box>
            </Box >
        </>
    )
}