import { v4 as uuidv4 } from 'uuid';
export const toast = {
    methods: {
        addToast(type, message) {
            var a = {
                id: uuidv4(),
                icontoast: this.$store.state.iconToast[type],
                classtoast: `toast-${type}`,
                type: type,
                message: message
            }
            this.$store.state.Toast.push(a)
        }
    },
}