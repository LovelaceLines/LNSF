import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { user as _user, role } from "@/types";
import { useRoleStore, useUserStore } from "@/store";
import { toValue } from "@/utils";

type user = _user & { confirmPassword: string };

export const useUserFormPage = () => {
	const { addUserToRole, getUser, postUser, putUser, removeUserFromRole } = useUserStore();
	const [role, setRole] = useState<role>();
	const { roles, getRoles } = useRoleStore();
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
		values: { id: 0, name: "", userName: "", email: "", phoneNumber: "", password: "", confirmPassword: "", roles: [] },
	});

	useEffect(() => {
		getRoles({ sort: "name", page: 1, perPage: 9999 });
	}, []);

	useEffect(() => {
		if (id) getUser(Number(id)).then((user) => toValue(user, setValue));
	}, [id]);

	const handleSave = (user: user) =>
		!id ? postUser(user).then((u) => toValue(u, setValue)) : putUser(user).then((u) => toValue(u, setValue));

	const handleAddUserToRole = () =>
		addUserToRole(watch("id") ?? 0, role?.id ?? 0).then((ur) =>
			setValue("roles", [...getValues().roles, { id: ur.roleId, name: role?.name ?? "" }])
		);

	const handleRemoveUserFromRole = (roleId: number) =>
		removeUserFromRole(watch("id") ?? 0, roleId).then(() =>
			setValue(
				"roles",
				getValues().roles.filter((r) => r.id !== roleId)
			)
		);

	return {
		id,
		roles,
		role,
		setRole,
		handleAddUserToRole,
		handleRemoveUserFromRole,
		control,
		register,
		handleSubmit,
		errors,
		getValues,
		setValue,
		watch,
		handleSave,
	};
};
