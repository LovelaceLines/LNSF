import { Drawer, useMediaQuery, useTheme } from "@mui/material";
import { AuthContext, useDrawerContext } from "../../Contexts";
import { Box } from '@mui/system';
import { useContext, useEffect, useState } from "react";
import { ListUlMenu } from "./Lists";
import nomelogo from '../../assets/logo_Variant4.svg';

interface ILateralMenuProps { children: React.ReactNode; }

export const LateralMenu: React.FC<ILateralMenuProps> = ({ children }) => {

    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const { isDrawerOpen, toggleDrawerOpen, drawerOptions, setDrawerOptions } = useDrawerContext();
    const [openIndex, setOpenIndex] = useState<number>(-1);

    const handleClick = (index: number) => {
        setOpenIndex(index === openIndex ? -1 : index);
    };
    const { user } = useContext(AuthContext);

    useEffect(() => {
        const menu = [
            {
                index: 1,
                icon: 'home',
                path: '/inicio',
                label: 'Início',
                options: [
                    {
                        pathOption: '/inicio',
                        labelOption: 'Início',
                    },
                ],
            },
            {
                index: 2,
                icon: 'apartment',
                path: '/inicio/apartamento',
                label: 'Apartamentos',
                options: [
                    {
                        pathOption: '/inicio/apartamentos/visualizar',
                        labelOption: 'Visualizar',
                    }

                ],
            },
            {
                index: 4,
                icon: 'description',
                path: '/inicio/registrodiario',
                label: 'Registro de saídas',
                options: [
                    {
                        pathOption: '/inicio/registrodiario/visualizar',
                        labelOption: 'Visualizar',
                    },
                    {
                        pathOption: '/inicio/registrodiario/adicionar',
                        labelOption: 'Adicionar',
                    }
                ],
            },
        ];
    
        if (user.role === 1 || user.role === 2 || user.role === 3) {
            menu[1].options.push(
                {
                    pathOption: '/inicio/apartamentos/gerenciar',
                    labelOption: 'Gerenciar',
                },
            )
            menu.push(
                {
                    index: 3,
                    icon: 'people',
                    path: '/inicio/pessoas',
                    label: 'Pessoas',
                    options: [
                        {
                            pathOption: '/inicio/pessoas/visualizar',
                            labelOption: 'Visualizar',
                        },
                    ],
                },
            )
            menu.push(
                {
                    index: 5,
                    icon: 'domain',
                    path: '/inicio/hospital',
                    label: 'Hospitais',
                    options: [
                        {
                            pathOption: '/inicio/hospital/visualizar',
                            labelOption: 'Visualizar',
                        },
                    ],
                },
            )
            menu.push(
                {
                    index: 6,
                    icon: 'vaccines',
                    path: '/inicio/tratamentos',
                    label: 'Tratamentos',
                    options: [
                        {
                            pathOption: '/inicio/tratamentos/visualizar',
                            labelOption: 'Visualizar',
                        },
                    ],
                },
            )
        }
        if (user.role === 1) {
            menu.push(
                {
                    index: 7,
                    icon: 'settings',
                    path: '/inicio/usuarios',
                    label: 'Usuários do sistema',
                    options: [
                        {
                            pathOption: '/inicio/usuarios/gerenciar',
                            labelOption: 'Gerenciar',
                        },
                    ],
                },
            )
        }
        setDrawerOptions(menu)
    }, [user.userName]);



    return (
        <>
            {/* menu lateral */}
            <Drawer open={isDrawerOpen} variant={smDown ? "temporary" : "permanent"} onClose={toggleDrawerOpen}>
                <Box
                    width={theme.spacing(32)}
                    height='100%'
                    display='flex'
                    flexDirection='column'
                    overflow='hidden'
                >
                    <Box
                        width='100%'
                        padding={theme.spacing(3)}
                        display='flex'
                        alignItems='center'
                        gap={theme.spacing(1)}
                    >
                        <img src={nomelogo} />
                    </Box>

                    {/* Itens do menu lateral */}
                    <Box flex={1}>
                        {drawerOptions.map(drawerOption => (
                            <ListUlMenu
                                key={drawerOption.path}
                                index={drawerOption.index}
                                icon={drawerOption.icon}
                                maintitle={drawerOption.label}
                                subItems={drawerOption.options}
                                openIndex={openIndex}
                                onClickCollapse={handleClick} />
                        ))}
                    </Box>
                </Box>
            </Drawer>

            <Box>
                {children}
            </Box>
        </>
    );
};