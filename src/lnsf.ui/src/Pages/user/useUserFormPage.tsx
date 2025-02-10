import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { user as _user, role } from "@/types";
import { useRoleStore, useUserStore } from "@/store";
import { toValue } from "@/utils";

type user = _user & { confirmPassword: string };

export const useUserFormPage = () => {
  const { id } = useParams<{ id: string | undefined }>();
  const {
    control,
    register,
    handleSubmit,
    formState: { errors },
    getValues,
    watch,
    setValue,
  } = useForm<user>({
    values: {
      id: 0,
      name: "",
      userName: "",
      email: "",
      phoneNumber: "",
      password: "",
      confirmPassword: "",
      roles: [],
    },
  });

  useEffect(() => {
    if (id) getUser(Number(id)).then((user) => toValue(user, setValue));
  }, [id]);

  const [role, setRole] = useState<role>();
  const { roles, getRoles } = useRoleStore();

  useEffect(() => {
    getRoles({ sort: "name", page: 1, perPage: 9999 });
  }, []);

  const { addUserToRole, getUser, postUser, putUser, removeUserFromRole } = useUserStore();

  const handleSave = (user: user) =>
    !getValues("id")
      ? postUser(user).then((u) => toValue(u, setValue))
      : putUser(user).then((u) => toValue(u, setValue));

  const handleAddUserToRole = () =>
    addUserToRole({ userId: getValues("id") ?? 0, roleId: role?.id ?? 0 }).then((ur) =>
      setValue("roles", [...getValues().roles, { id: ur.roleId, name: role?.name ?? "" }])
    );

  const handleRemoveUserFromRole = (roleId: number) =>
    removeUserFromRole({ userId: getValues("id") ?? 0, roleId: roleId }).then(() =>
      setValue(
        "roles",
        getValues().roles.filter((r) => r.id !== roleId)
      )
    );

  return {
    roles,
    role,
    setRole,
    handleSave,
    handleAddUserToRole,
    handleRemoveUserFromRole,
    control,
    errors,
    getValues,
    handleSubmit,
    register,
    setValue,
    watch,
  };
};
